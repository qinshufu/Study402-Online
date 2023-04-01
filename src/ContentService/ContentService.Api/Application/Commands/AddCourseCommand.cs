using MediatR;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands
{
    /// <summary>
    /// 添加课程命令
    /// </summary>
    public class AddCourseCommand : IRequest<CourseInformationModel>
    {
        public AddCourseModel Model { get; }

        public AddCourseCommand(AddCourseModel model)
        {
            Model = model;
        }
    }
}