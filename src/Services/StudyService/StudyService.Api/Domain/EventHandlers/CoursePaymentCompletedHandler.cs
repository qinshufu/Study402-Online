using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Events;
using Study402Online.Common.Helpers;
using Study402Online.StudyService.Api.Instructure;
using Study402Online.StudyService.Api.Models.DataModels;

namespace Study402Online.StudyService.Api.Domain.EventHandlers;

/// <summary>
/// 课程支付完成事件处理器
/// </summary>
public class CoursePaymentCompletedHandler : IConsumer<CoursePaymentCompletedEvent>
{
    private readonly StudyDbContext _dbContext;

    public CoursePaymentCompletedHandler(StudyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<CoursePaymentCompletedEvent> context)
    {
        var courseSelection = await _dbContext
            .CourseSelectionRecords
            .SingleOrDefaultAsync(r => r.Id == int.Parse(context.Message.CourseSelectionId)
                && r.Status == Models.DataModels.CourseSelectionStatus.Unpaid);

        if (courseSelection is null)
            return;

        if (context.Message.IsPaymentSuccessful)
            await AddCourseToClassScheduleAsync(courseSelection);
    }

    private async Task AddCourseToClassScheduleAsync(CourseSelectionRecord selection)
    {
        selection.Status = CourseSelectionStatus.Successful;
        selection.ToTakeEffectTime = DateTime.Now;
        selection.ExpirationTime = DateTime.Now.AddDays(selection.ValidDays);

        _dbContext.Update(selection);

        var classSchedule = new ClassSchedule();

        PocoHelper.CopyProperties(selection, classSchedule);
        classSchedule.CreateTime = DateTime.Now;
        classSchedule.Id = default;

        _dbContext.Add(classSchedule);
        _ = await _dbContext.SaveChangesAsync();
    }
}
