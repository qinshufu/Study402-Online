using Aliyun.OSS;
using Dapper;
using FFMpegCore;
using FFMpegCore.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Study402Online.MediaService.Model.DataModel;
using System.Data.Common;

namespace Study402Online.MediaService.CodingConversionService;

/// <summary>
/// 视频格式转换后台服务
/// </summary>
/// <remarks>
/// 将视频文件转换为 H.264 编码的 MP4 文件
/// </remarks>
public class VideoConversionWorker : BackgroundService
{
    private readonly DbConnection _db;

    private readonly ConnectionMultiplexer _redis;
    private readonly OssClient _oss;
    private readonly OssOptions _options;
    private readonly Semaphore _semaphore = new Semaphore(_concurrent, _concurrent);

    private readonly static int _concurrent = Environment.ProcessorCount + 1;

    public VideoConversionWorker(DbConnection db, ConnectionMultiplexer redis, OssClient oss, IOptions<OssOptions> options)
    {
        _db = db;
        _redis = redis;
        _oss = oss;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            _semaphore.WaitOne();

            var objectId = await _db.QuerySingleOrDefaultAsync<string>(@"select top 1 MediaFile from MediaProcess order by newid()");

            ThreadPool.QueueUserWorkItem(async (_) =>
            {
                try
                {
                    await ProcessVideoAsync(objectId).ConfigureAwait(false);
                }
                finally
                {
                    _semaphore.Release();
                }
            });
        }
    }

    private async Task ProcessVideoAsync(string objectId)
    {
        // 判断是否有其他并行的 Worker 在处理响应的视频
        var redisDb = _redis.GetDatabase();
        var isSingle = await redisDb.StringSetAsync(objectId, 1, TimeSpan.FromHours(1), When.NotExists);

        // 有其他的服务实例在处理该视频
        if (isSingle is false)
            return;

        try
        {
            var tempPath = Path.GetTempFileName();
            var outputPath = Path.GetTempFileName();

            using var tempfile = new FileStream(tempPath, FileMode.OpenOrCreate);
            _ = _oss.GetObject(new GetObjectRequest(_options.FileBucket, objectId), tempfile);

            var processSuccess = await FFMpegArguments
                   .FromFileInput(tempPath)
                   .OutputToFile(outputPath, true, options => options
                       .WithVideoCodec(VideoCodec.LibX264)
                       .WithConstantRateFactor(23)
                       .WithAudioCodec(AudioCodec.Aac)
                       .WithAudioBitrate(128)
                       .WithFastStart())
                   .ProcessAsynchronously();

            if (processSuccess)
            {
                await MoveToProcessHistoryAsync(objectId);

                ReplaceOssFile(objectId, outputPath);
            }
            else
            {
                await MarkVideoProcessFailureAsync(objectId);

                // 重试超过三次以后放弃
                var retryCount = await _db.ExecuteScalarAsync<int>("select FailureCount from MediaProcess where MediaFile = @objKey");
                if (retryCount >= 3)
                {
                    await MoveToProcessHistoryAsync(objectId);
                }
            }
        }
        finally
        {
            await redisDb.KeyDeleteAsync(objectId);
        }
    }

    private void ReplaceOssFile(string objectId, string file)
    {
        _ = _oss.DeleteObject(new(_options.BigFileBucket, objectId));

        var fileinfo = new FileInfo(file);
        using var stream = new FileStream(file, FileMode.Open);
        _ = _oss.PutBigObject(_options.BigFileBucket, objectId, stream, new ObjectMetadata { ContentLength = fileinfo.Length });
    }

    private async Task MarkVideoProcessFailureAsync(string objectId)
    {
        // 标记失败次数
        var sql = @"
begin tran;
update MediaProcess set FailureCount = FailureCount + 1 where MediaFile = @objKey;
commit;
";
        await _db.ExecuteAsync(sql, new { ObjectId = objectId });
    }

    private Task MoveToProcessHistoryAsync(string objectId)
    {
        var sql = @"
begin tran;

insert into MediaProcessHistory(
    id, mediafile, mediafilename, storagebucket, 
    status, uploadtime, CompletionTime, AccessPath, FailureMessage, FailureCount)
select [Id],[MediaFile],[MediaFileName]
      ,[StorageBucket],[Status],[UploadTime]
      ,[CompletionTime],[AccessPath],[FailureMessage], FailureCount
from MediaProcess
where MediaProcess.MediaFile = @objKey;

delete from MediaProcess where MediaFile = @objKey
commit;
";
        return _db.ExecuteAsync(sql, new { ObjectId = objectId });
    }

    public override void Dispose()
    {
        _redis.Dispose();
        _semaphore.Dispose();
    }
}
