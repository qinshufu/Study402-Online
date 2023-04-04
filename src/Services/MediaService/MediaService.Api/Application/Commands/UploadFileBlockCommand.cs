using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 上传文件快命令
/// </summary>
public class UploadFileBlockCommand : IRequest<Result<bool>>
{
    /// <summary>
    /// 文件 Hash
    /// </summary>
    public string FileHash { get; set; }

    /// <summary>
    /// 块 Hash
    /// </summary>
    public string BlockHash { get; set; }

    /// <summary>
    /// 块序号
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 文件块
    /// </summary>
    public IFormFile FileBlock { get; set; }
}