using MediatR;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 添加课程计划命令
/// </summary>
public class AddTeachPlanCommand : IRequest<TeachPlan>
{
    public AddTeachPlanCommand(AddTeachPlanModel model)
    {
        Model = model;
    }

    public AddTeachPlanModel Model { get; }
}
