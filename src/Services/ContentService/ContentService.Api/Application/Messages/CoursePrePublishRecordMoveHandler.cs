using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Messages;

/// <summary>
/// 移动课程预发布记录消息处理器
/// </summary>
public class CoursePrePublishRecordMoveHandler : IMessageHandler<CoursePrePublishRecordMoveMessage>
{
    ContentDbContext DbContext { get; }

    IMapper Mapper { get; }

    public CoursePrePublishRecordMoveHandler(ContentDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task HandleAsync(CoursePrePublishRecordMoveMessage message, CancellationToken cancellationToken = default)
    {
        var course = await DbContext.Courses.SingleAsync(c => c.Id == message.RecordId);

        var publish = Mapper.Map<CoursePublish>(course);
        course.Id = default;

        await DbContext.CoursePublishes.AddAsync(publish);
        await DbContext.coursePublishPres.Where(c => c.Id == message.RecordId).ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();
        await DbContext.Database.CommitTransactionAsync();
    }
}
