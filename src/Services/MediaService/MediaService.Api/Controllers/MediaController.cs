using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Application.Commands;
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
    public Task<Result<MediaFile>> UploadFile([FromForm] string fileHash, [FromForm] IFormFile file)
    {
        var command = new UploadFileCommand() { File = file, FileHash = fileHash };
        return _mediator.Send(command);
    }

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