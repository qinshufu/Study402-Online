using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Queries;

/// <summary>
/// 获取课程计划命令
/// </summary>
public record TeachPlanQuery : IRequest<Result<TeachPlanInfo>>
{
    /// <summary>
    /// 课程计划 ID
    /// </summary>
    public int TeachPlanId { get; init; }
}
