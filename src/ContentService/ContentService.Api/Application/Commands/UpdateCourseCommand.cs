using MediatR;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;


/// <summary>
/// 更新课程命令
/// </summary>
public class UpdateCourseCommand : IRequest<CourseInformationModel>
{
    public UpdateCourseCommand(UpdateCourseModel model) => Model = model;

    public UpdateCourseModel Model { get; }
}
