using MediatR;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Model;
using Study402Online.StudyService.Api.HttpClients;
using Study402Online.StudyService.Api.Instructure;
using Study402Online.StudyService.Api.Models.ViewModels;
using Study402Online.UserService.Model.ViewModels;

namespace Study402Online.StudyService.Api.Application.Commands;

/// <summary>
/// 获取课程视频命令处理器
/// </summary>
public class CourseVideoGetHandler : IRequestHandler<CourseVideoGetCommand, Result<VideoInfo>>
{
    private readonly ICourseServiceClient _courseServiceClient;

    private readonly ITeachPlanServiceClient _teachPlanServiceClient;

    private readonly IMediaServiceClient _mediaServiceClient;

    private readonly HttpContext _httpContext;

    private readonly StudyDbContext _dbContext;

    public CourseVideoGetHandler(
        ICourseServiceClient courseServiceClient,
        ITeachPlanServiceClient teachPlanServiceClient,
        IMediaServiceClient mediaServiceClient,
        IHttpContextAccessor httpContextAccessor,
        StudyDbContext dbContext)
    {
        _courseServiceClient = courseServiceClient;
        _teachPlanServiceClient = teachPlanServiceClient;
        _mediaServiceClient = mediaServiceClient;
        _httpContext = httpContextAccessor.HttpContext!;
        _dbContext = dbContext;
    }

    public async Task<Result<VideoInfo>> Handle(CourseVideoGetCommand request, CancellationToken cancellationToken)
    {
        // 1. 如果是免费课程，登录用户可以直接学习
        // 2. 如果是收费课程，但章节可以试学，则可以学习
        // 3. 如果是收费课程，但登录用户已经购买，则可以学习
        // 4. 否则不能够学习

        var courseResult = await _courseServiceClient.GetCourseInfoAsync(request.CourseId);

        if (courseResult.Success is false || courseResult.Value is null)
            return ResultFactory.Fail<VideoInfo>("获取课程失败");

        var teachPlanResult = await _teachPlanServiceClient.GetTeachPlanAsync(request.TeachPlanId);

        if (teachPlanResult.Success is false || teachPlanResult.Value is null)
            return ResultFactory.Fail<VideoInfo>("获取课程计划失败");

        var fileResult = await _mediaServiceClient.GetMediaFileAsync(request.MediaId);

        if (fileResult.Success is false || teachPlanResult.Value is null)
            return ResultFactory.Fail<VideoInfo>("获取视频文件失败");

        var videoInfo = new VideoInfo
        {
            IsFreeCourse = courseResult.Value.Price == 0,
            IsTrial = teachPlanResult.Value.Preview ?? false,
            Url = new Uri(fileResult.Value.AccessPath)
        };

        var userId = _httpContext.User.Claims.Single(c => c.Issuer == nameof(UserInfo.Id)).Value;
        var classSchudule = await _dbContext.ClassSchedules.SingleOrDefaultAsync(s => s.UserId == int.Parse(userId) && s.CourseId == request.CourseId);

        if (classSchudule is not null && classSchudule.ExpirationTime < DateTime.Now)
            return ResultFactory.Success(videoInfo);

        if (courseResult.Value.Price is 0)
            return ResultFactory.Success(videoInfo);

        if (teachPlanResult.Value.Preview ?? false)
            return ResultFactory.Success(videoInfo);

        return ResultFactory.Fail<VideoInfo>("无法获取该视频，请先购买或者续期相应课程");
    }
}
