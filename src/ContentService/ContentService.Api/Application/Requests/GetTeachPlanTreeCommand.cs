using MediatR;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Requests;

/// <summary>
/// 获取课程计划命令
/// </summary>
/// <param name="Course"></param>
public record GetTeachPlanTreeCommand(int Course) : IRequest<List<TeachPlanTreeNode>>;