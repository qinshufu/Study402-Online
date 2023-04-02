using MediatR;
using Study402Online.Common.Model;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Queries;

/// <summary>
/// 获取课程计划命令
/// </summary>
/// <param name="Course"></param>
public record TeachPlanTreeQuery(int Course) : IRequest<Result<List<TeachPlanTreeNode>>>;