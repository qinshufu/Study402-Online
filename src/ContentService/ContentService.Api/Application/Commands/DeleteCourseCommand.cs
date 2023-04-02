using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 删除课程命令
/// </summary>
/// <param name="Course"></param>
public record DeleteCourseCommand(int Course) : IRequest<Result<object>>;
