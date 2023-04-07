using MediatR;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 上传文件，并将其保存到指定路径
/// </summary>
public class UploadAndSaveFileCommand : IRequest<Result<MediaFile>>
{
    /// <summary>
    /// 文件
    /// </summary>
    public IFormFile File { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string SavePath { get; set; }
}