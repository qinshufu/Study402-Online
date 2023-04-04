using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Infrastructure;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// 测试分块是否存在命令
/// </summary>
public class TestFileBlockExistsCommandHandler : IRequestHandler<TestFileBlockExistsCommand, Result<bool>>
{
    private readonly MediaServiceDbContext _context;

    public TestFileBlockExistsCommandHandler(MediaServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(TestFileBlockExistsCommand request, CancellationToken cancellationToken) =>
        ResultFactory.Success(await _context.FileBlocks.Where(b => b.BlockHash == request.BlockHash).AnyAsync());
}
