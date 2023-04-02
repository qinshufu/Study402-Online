using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Queries;

/// <summary>
/// 获取课程信息命令
/// </summary>
/// <param name="Id"></param>
public record CourseQuery(int Id) : IRequest<Result<CourseInformationModel>>;
