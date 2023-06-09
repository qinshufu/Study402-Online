using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 添加教师命令
/// </summary>
public class AddTeacherCommand : IRequest<Result<CourseTeacherRelation>>
{
    public AddTeacherModel Model { get; }

    public AddTeacherCommand(AddTeacherModel model) => Model = model;
}
