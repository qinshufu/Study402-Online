using MassTransit.Futures.Contracts;
using Refit;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.StudyService.Api.HttpClients;

/// <summary>
/// 教学计划接口（内容服务）
/// </summary>
public interface ITeachPlanServiceClient
{
    [Get("/api/teach-plan/get")]
    Task<Result<TeachPlanInfo>> GetTeachPlanAsync([Query("teachPlanId")] int id);
}
