using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 发布课程命令
/// </summary>
/// <param name="Course"></param>
public record PublishCourseCommand(int Course) : IRequest<UnitResult>;