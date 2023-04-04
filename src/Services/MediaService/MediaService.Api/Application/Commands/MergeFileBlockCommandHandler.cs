using Aliyun.OSS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Application.Configurations;
using Study402Online.MediaService.Api.Infrastructure;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 合并文件块命令处理器
/// </summary>
public class MergeFileBlockCommandHandler : IRequestHandler<MergeFileBlockCommand, Result<MediaFile>>
{
    private readonly MediaServiceDbContext _context;
    private readonly OssClient _ossClient;
    private readonly OssOptions _options;

    public MergeFileBlockCommandHandler(MediaServiceDbContext context, OssClient ossClient, IOptions<OssOptions> options)
    {
        _context = context;
        _ossClient = ossClient;
        _options = options.Value;
    }

    public async Task<Result<MediaFile>> Handle(MergeFileBlockCommand request, CancellationToken cancellationToken)
    {
        // 分块上传文件的实现（这个算法）这里有一个问题就是，需要保证同一个文件分块的数量是稳定的，就是别老改分块的算法
        var objectName = $"{request.FileHash[0..2]}/{request.FileHash[2..4]}/{request.FileHash}";
        var uploadRecord = await _context.ChunkUploads.Where(x => x.FileHash == request.FileHash).SingleOrDefaultAsync(cancellationToken);

        // 提前结束事务，因为接下来有一个网络请求
        await _context.Database.CommitTransactionAsync(cancellationToken);

        if (uploadRecord is null)
            return ResultFactory.Fail<MediaFile>("无法合并文件，文件块上传记录不存在");

        _ = _ossClient.CompleteMultipartUpload(new CompleteMultipartUploadRequest(_options.BigFileBucket, objectName, uploadRecord.UploadId));

        uploadRecord.Status = ChunkUploadStatus.Complete;

        var media = new MediaFile
        {
            // TODO 一些字段没有设置
            FileId = request.FileHash,
            AccessPath = $"https://{_options.BigFileBucket}.{_options.Endpoint}/{objectName}",
            FileName = request.FileName,
            StoragePath = objectName,
        };

        _context.Add(media);
        await _context.SaveChangesAsync();

        var mediaProcess = new MediaProcess
        {
            MediaFile = media.Id,
            MediaFileName = request.FileName,
            StorageBucket = _options.FileBucket,
            AccessPath = $"https://{_options.BigFileBucket}.{_options.Endpoint}/{objectName}"
        };
        _context.Add(mediaProcess);
        await _context.SaveChangesAsync();

        // 上传完成后删除文件数据块及其记录，实际上还需要实现删除超时的上传任务：
        // TODO 当一个上传操作超过了一定的时间，应该删除对应的数据块以及记录

        var blocks = await _context.FileBlocks.Where(b => b.FileHash == request.FileHash).ToListAsync();
        _ = await _context.FileBlocks.Where(b => b.FileHash == request.FileHash).ExecuteDeleteAsync();
        _ = _ossClient.DeleteObjects(new(_options.BigFileBucket, blocks.Select(b => b.ObjectKey).ToList()));

        // TODO 这里有一个问题就是事务持续时间太长了

        return ResultFactory.Success(media);
    }
}
