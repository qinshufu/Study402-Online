using Aliyun.OSS;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using RazorLight;
using StackExchange.Redis;
using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Application.Configurations;
using Study402Online.ContentService.Api.Application.Messages;
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
    private readonly IMessagePublisher _messagePublisher;

    public PublishCourseCommandHandler(
        ContentDbContext context,
        IMapper mapper,
        ITemplateService templateService,
        ICacheService cacheService,
        IMediaService mediaService,
        ISearchService searchService,
        IMessagePublisher messagePublisher)
    {
        _context = context;
        _mapper = mapper;
        _mediaService = mediaService;
        _searchService = searchService;
        _cacheService = cacheService;
        _templateService = templateService;
        _messagePublisher = messagePublisher;
    }

    public async Task<UnitResult> Handle(PublishCourseCommand request, CancellationToken cancellationToken)
    {
        var publishpre = await _context.coursePublishPres
            .Where(p => p.Status == 1) // 审核通过
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync(p => p.CourseId == request.Course);

        if (publishpre is null)
            return ResultFactory.Fail("没有已通过的课程审核记录");

        var course = await _context.Courses.SingleAsync(c => c.Id == publishpre.CourseId);

        await _messagePublisher.PublishAsync(new CoursePrePublishRecordMoveMessage(publishpre.Id));

        await _messagePublisher.PublishAsync(new PreviewPageRenderAndSaveMessage(publishpre.CourseId));

        await _messagePublisher.PublishAsync(new CourseCacheMessage(course));

        await _messagePublisher.PublishAsync(new CourseToEsSaveMessage(course));

        return ResultFactory.Success();
    }
}