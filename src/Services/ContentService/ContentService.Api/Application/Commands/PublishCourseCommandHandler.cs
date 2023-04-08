using Aliyun.OSS;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using RazorLight;
using StackExchange.Redis;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Application.Configurations;
using Study402Online.ContentService.Api.Application.Services;
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
    private readonly IMediaService _mediaService;
    private readonly ISearchService _searchService;
    private readonly ICacheService _cacheService;
    private readonly ITemplateService _templateService;

    public PublishCourseCommandHandler(
        ContentDbContext context,
        IMapper mapper,
        ITemplateService templateService,
        ICacheService cacheService,
        IMediaService mediaService,
        ISearchService searchService)
    {
        _context = context;
        _mapper = mapper;
        _mediaService = mediaService;
        _searchService = searchService;
        _cacheService = cacheService;
        _templateService = templateService;
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

        // 移动记录到发布表
        var course = await _context.Courses.SingleAsync(c => c.Id == request.Course);

        var publish = _mapper.Map<CoursePublish>(publishpre);
        await _context.CoursePublishes.AddAsync(publish);

        // 生成预览页面并存储到OSS
        var previewPageResult = await _templateService.RenderAsync("Course", course);
        if (previewPageResult is { Success: true })
            _ = await _mediaService.UploadFileAsync($"/preview/course/{request.Course}", previewPageResult.Value);
        previewPageResult.Value.Close();

        // 缓存课程信息到 Redis
        await _cacheService.CacheRowAsync(course);

        // 将课程存储到 ES
        await _searchService.AddCourseDocumentAsync(course);


        return ResultFactory.Success();
    }
}