using MediatR;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Requests;

/// <summary>
/// 获取课程信息命令
/// </summary>
/// <param name="Id"></param>
public record GetCourseQuery(int Id) : IRequest<CourseInformationModel>;
