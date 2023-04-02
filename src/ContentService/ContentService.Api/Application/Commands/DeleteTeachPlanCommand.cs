using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 删除课程计划命令
/// </summary>
/// <param name="TeachPlan"></param>
public record DeleteTeachPlanCommand(int TeachPlan) : IRequest<Result<int>>;
