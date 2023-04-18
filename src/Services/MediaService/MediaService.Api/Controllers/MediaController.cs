using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Application.Commands;
using Study402Online.MediaService.Api.Application.Queries;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Controllers;

[Route("/api/media")]
[ApiController]
public class MediaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MediaController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    public Task<Result<MediaFile>> UploadFile([FromForm] string? fileHash, [FromForm] string? path, [FromForm] IFormFile file)
    {
        IRequest<Result<MediaFile>> command = (fileHash, path) switch
        {
            (string f, null) when f is not null => new UploadAndSaveFileCommand { File = file, SavePath = f },
            (null, string h) when h is not null => new UploadFileCommand { File = file, FileHash = h },
            _ => throw new ArgumentNullException(nameof(fileHash), "至少提供 fileHash 和 path 参数中的一个"),
        };

        return _mediator.Send(command);
    }

    /// <summary>
    /// 获取媒体文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("get")]
    public Task<Result<MediaFile>> GetFile([FromQuery] MediaFileQuery query) => _mediator.Send(query);

    /// <summary>
    /// 测试文件块是否存在
    /// </summary>
    /// <param name="blockHash"></param>
    /// <returns></returns>
    [HttpPost("test-fileBlock-block")]
    public Task<Result<bool>> TestFileBlock([FromBody] string fileHash, [FromBody] string blockHash)
    {
        var command = new TestFileBlockExistsCommand() { FileHash = fileHash, BlockHash = blockHash };
        return _mediator.Send(command);
    }

    /// <summary>
    /// 上传文件块
    /// </summary>
    /// <param name="fileHash"></param>
    /// <param name="order"></param>
    /// <param name="fileBlock"></param>
    /// <returns></returns>
    [HttpPost("upload-fileBlock-block")]
    public Task<Result<bool>> UploadFileBlock([FromForm] string fileHash, [FromForm] string blockHash, [FromForm] int order, [FromForm] IFormFile fileBlock)
    {
        var command = new UploadFileBlockCommand { FileHash = fileHash, BlockHash = blockHash, Order = order, FileBlock = fileBlock };
        return _mediator.Send(command);
    }

    /// <summary>
    /// 合并文件块
    /// </summary>
    /// <param name="fileHash"></param>
    /// <returns></returns>
    [HttpPost("merge-fileBlock-block")]
    public Task<Result<MediaFile>> MergeFileBlock([FromBody] string fileHash, [FromBody] string fileName)
    {
        var command = new MergeFileBlockCommand { FileHash = fileHash, FileName = fileName };
        return _mediator.Send(command);
    }
}