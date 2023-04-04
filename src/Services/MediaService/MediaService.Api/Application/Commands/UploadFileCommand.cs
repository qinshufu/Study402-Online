using MediatR;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 上传文件命令
/// </summary>
public record UploadFileCommand : IRequest<Result<MediaFile>>
{
    public IFormFile File { get; init; }

    public string FileHash { get; init; }
}
