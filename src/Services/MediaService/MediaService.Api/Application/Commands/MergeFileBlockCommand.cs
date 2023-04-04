using MediatR;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 合并文件块命令
/// </summary>
public class MergeFileBlockCommand : IRequest<Result<MediaFile>>
{
    public string FileName { get; init; }

    public string FileHash { get; init; }
}