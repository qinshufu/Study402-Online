using Aliyun.OSS;
using MediatR;
using Microsoft.Extensions.Options;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Application.Configurations;
using Study402Online.MediaService.Api.Infrastructure;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;


/// <summary>
/// 上传文件处理器
/// </summary>
public class UploadAndSaveFileCommandHandler : IRequestHandler<UploadAndSaveFileCommand, Result<MediaFile>>
{
    private readonly MediaServiceDbContext _context;
    private readonly OssClient _ossClient;
    private readonly OssOptions _options;

    public UploadAndSaveFileCommandHandler(MediaServiceDbContext context, OssClient ossClient, IOptions<OssOptions> options)
    {
        _context = context;
        _ossClient = ossClient;
        _options = options.Value;
    }


    public async Task<Result<MediaFile>> Handle(UploadAndSaveFileCommand request, CancellationToken cancellationToken)
    {
        using var fileStream = request.File.OpenReadStream();
        _ossClient.PutObject(new(_options.FileBucket, request.SavePath, fileStream));

        var media = new MediaFile
        {
            FileId = request.SavePath,
            AccessPath = $"https://{_options.FileBucket}.{_options.Endpoint}/{request.SavePath}",
            FileName = request.File.Name,
            StoragePath = request.SavePath
        };

        await _context.AddAsync(media, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var mediaProcess = new MediaProcess
        {
            AccessPath = $"https://{_options.FileBucket}.{_options.Endpoint}/{request.SavePath}",
            MediaFile = media.Id,
            MediaFileName = media.FileName,
            StorageBucket = _options.FileBucket
        };

        await _context.AddAsync(mediaProcess, cancellationToken);

        return ResultFactory.Success(media);
    }
}
