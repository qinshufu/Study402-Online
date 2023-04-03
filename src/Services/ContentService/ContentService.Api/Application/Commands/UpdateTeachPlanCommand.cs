using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 更新课程计划命令
/// </summary>
public record UpdateTeachPlanCommand(UpdateTeachPlanModel Model) : IRequest<Result<TeachPlan>>;