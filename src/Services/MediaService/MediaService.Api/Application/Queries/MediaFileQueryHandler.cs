using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Infrastructure;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Queries;

/// <summary>
/// 媒体文件查询处理器
/// </summary>
public class MediaFileQueryHandler : IRequestHandler<MediaFileQuery, Result<MediaFile>>
{
    private readonly MediaServiceDbContext _dbContext;

    public MediaFileQueryHandler(MediaServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<MediaFile>> Handle(MediaFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _dbContext.MediaFiles.SingleOrDefaultAsync(m => m.Id == request.Id);

        if (file is null)
            return ResultFactory.Fail<MediaFile>("指定媒体文件不文件不存在");

        if (file.AuditStatus is not "审核完成")
            return ResultFactory.Fail<MediaFile>("媒体文件没有审核完成！");

        if (file.Status is not "转码完成")
            return ResultFactory.Fail<MediaFile>("媒体文件没有转码完成！");

        return ResultFactory.Success(file);
    }
}
