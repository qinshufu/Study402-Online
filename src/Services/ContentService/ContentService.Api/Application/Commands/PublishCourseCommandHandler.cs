using Aliyun.OSS;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RazorLight;
using StackExchange.Redis;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Application.Configurations;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 发布课程命令处理器
/// </summary>
public class PublishCourseCommandHandler : IRequestHandler<PublishCourseCommand, UnitResult>
{
    private readonly ContentDbContext _context;
    private readonly IMapper _mapper;
    private readonly RazorLightEngine _razorEngine;
    private readonly OssClient _oss;
    private readonly IOptions<OssOptions> _options;
    private readonly ConnectionMultiplexer _redis;

    public PublishCourseCommandHandler(
        ContentDbContext context, IMapper mapper, RazorLightEngine razorEngine,
        OssClient oss, IOptions<OssOptions> options, ConnectionMultiplexer redis)
    {
        _context = context;
        _mapper = mapper;
        _razorEngine = razorEngine;
        _oss = oss;
        _options = options;
        _redis = redis;
    }

    public async Task<UnitResult> Handle(PublishCourseCommand request, CancellationToken cancellationToken)
    {
        var publishpre = await _context.coursePublishPres
            .Where(p => p.Status == 1) // 审核通过
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync(p => p.CourseId == request.Course);

        if (publishpre is null)
            return ResultFactory.Fail("没有已通过的课程审核记录");

        // TODO 这样的包含远程请求的事务，最好修改成为 SAGE ，可能

        // TODO 移动记录到发布表
        var course = await _context.Courses.SingleAsync(c => c.Id == request.Course);

        var publish = _mapper.Map<CoursePublish>(publishpre);
        await _context.CoursePublishes.AddAsync(publish);

        // TODO 生成预览页面并存储到OSS
        var previewPage = await _razorEngine.CompileRenderStringAsync("Course", "Course.rz", course);
        using var previewPageStream = new MemoryStream(Encoding.UTF8.GetBytes(previewPage));
        var previewPageHash = SHA256.HashDataAsync(previewPageStream);
        var previewPageObjectId = $"page/{previewPageHash[0..2]}/{previewPageHash[2..4]}/{previewPageHash}";

        _ = _oss.PutObject(_options.Value.FileBucket, previewPageObjectId, previewPageStream);

        // TODO 缓存课程信息到 Redis
        var db = _redis.GetDatabase();
        await db.StringSetAsync($"course:{course.Id}", JsonSerializer.Serialize(course));

        // TODO 将课程存储到 ES

        return ResultFactory.Success();
    }
}