using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.ViewModel;
using Study402Online.StudyService.Api.HttpClients;
using Study402Online.StudyService.Api.Instructure;
using Study402Online.StudyService.Api.Models.DataModels;

namespace Study402Online.StudyService.Api.Application.Commands;

/// <summary>
/// 选课命令处理器
/// </summary>
public class CourseToScheduleAddHandler : IRequestHandler<CourseToScheduleAddCommand, UnitResult>
{
    private ICourseServiceClient _courseServiceClient;
    private readonly StudyDbContext _dbContext;

    public CourseToScheduleAddHandler(ICourseServiceClient courseServiceClient, StudyDbContext dbContext)
    {
        _courseServiceClient = courseServiceClient;
        _dbContext = dbContext;
    }

    public async Task<UnitResult> Handle(CourseToScheduleAddCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseServiceClient.GetCourseInfoAsync(request.Course);

        if (result.Success is false)
            return ResultFactory.Fail("选课失败，有可能是课程不存在");

        var course = result.Value;

        // 这里的 Chargeting 实际上应该是字典服务里面的课程收费类型列出的相关字段，不过我后来发现这个实际上是自找麻烦
        // 实际上直接使用枚举就非常简单了，这里直接使用 “免费” 和 “收费” 的汉字来标识是因为我不想改代码了，实际上就是
        // 简单编写一下逻辑，关于 DictionService 是什么会在 README 中详细说明
        if (course.Chargeting == "收费")
            return ResultFactory.Fail("仅可直接添加免费课程");

        // 相同的选课是否存在，如果还有其他有效的相同选课则不能添加
        var isCourseInSchedule = await _dbContext.ClassSchedules.AnyAsync(c => c.CourseId == course.Id);

        if (isCourseInSchedule)
            return ResultFactory.Fail("不能添加选课，因为课程表中已有相同选课");

        var selectionRecord = new CourseSelectionRecord()
        {
            IsPaidCourse = false,
            CompanyId = 1234,
            CourseId = course.Id,
            UserId = 1234,
            CourseName = course.Name,
            CreateTime = DateTime.Now,
            ToTakeEffectTime = DateTime.Now,
            ExpirationTime = DateTime.Now.Add(TimeSpan.FromDays(course.ValidDays ?? 365)),
            Remarks = course.Description,
            UserName = "测试",
            ValidDays = course.ValidDays ?? 365,
            Status = CourseSelectionStatus.Successful
        };

        await _dbContext.CourseSelectionRecords.AddAsync(selectionRecord);
        await _dbContext.SaveChangesAsync();

        await _dbContext.ClassSchedules.AddAsync(new()
        {
            CompanyId = 1234,
            CourseId = course.Id,
            UserId = 1234,
            CourseName = course.Name,
            CreateTime = DateTime.Now,
            ToTakeEffectTime = DateTime.Now,
            ExpirationTime = DateTime.Now.Add(TimeSpan.FromDays(course.ValidDays ?? 365)),
            Remarks = course.Description,
            CourseSelectionId = selectionRecord.Id,
        });

        await _dbContext.SaveChangesAsync();

        return ResultFactory.Success<UnitResult>("选课成功");
    }
}
