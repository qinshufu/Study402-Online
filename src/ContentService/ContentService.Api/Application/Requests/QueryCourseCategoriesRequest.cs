using MediatR;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Requests
{
    /// <summary>
    /// 查询课程分类请求
    /// </summary>
    public class QueryCourseCategoriesRequest : IRequest<CourseCategoriesTreeModel>
    {
        public QueryCourseCategoriesRequest(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}