using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 测试文件块是否存在命令
/// </summary>
public record TestFileBlockExistsCommand : IRequest<Result<bool>>
{
    public string FileHash { get; init; }
    public string BlockHash { get; init; }
}