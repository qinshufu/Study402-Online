using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;


/// <summary>
/// 更新课程命令
/// </summary>
public class UpdateCourseCommand : IRequest<Result<CourseInformationModel>>
{
    public UpdateCourseCommand(UpdateCourseModel model) => Model = model;

    public UpdateCourseModel Model { get; }
}
