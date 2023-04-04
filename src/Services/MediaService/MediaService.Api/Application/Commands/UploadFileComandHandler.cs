using Aliyun.OSS;
using MediatR;
using Microsoft.Extensions.Options;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Application.Configurations;
using Study402Online.MediaService.Api.Infrastructure;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 上传文件命令处理器
/// </summary>
public class UploadFileComandHandler : IRequestHandler<UploadFileCommand, Result<MediaFile>>
{
    private readonly MediaServiceDbContext _context;

    private readonly OssClient _ossClient;
    private readonly OssOptions _options;

    public UploadFileComandHandler(MediaServiceDbContext context, OssClient ossClient, IOptions<OssOptions> options)
    {
        _context = context;
        _ossClient = ossClient;
        _options = options.Value;
    }

    public async Task<Result<MediaFile>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var objName = $"{request.FileHash[0..2]}/{request.FileHash[2..4]}/{request.FileHash}";
        using var fileStream = request.File.OpenReadStream();
        _ossClient.PutObject(new(_options.FileBucket, objName, fileStream));

        var media = new MediaFile
        {
            FileId = objName,
            AccessPath = $"https://{_options.FileBucket}.{_options.Endpoint}/{objName}",
            FileName = request.File.Name,
            StoragePath = objName
        };

        await _context.AddAsync(media, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var mediaProcess = new MediaProcess
        {
            AccessPath = $"https://{_options.FileBucket}.{_options.Endpoint}/{objName}",
            MediaFile = media.Id,
            MediaFileName = media.FileName,
            StorageBucket = _options.FileBucket
        };

        await _context.AddAsync(mediaProcess, cancellationToken);

        return ResultFactory.Success(media);
    }
}
