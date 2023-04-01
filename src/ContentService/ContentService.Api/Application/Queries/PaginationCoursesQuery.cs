using MediatR;
using Study402Online.Common.Expressions;
using Study402Online.Common.Linq;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;
using System.Linq.Expressions;

namespace Study402Online.ContentService.Api.Application.Queries
{
    /// <summary>
    /// 课程分页查询
    /// </summary>
    public class PaginationCoursesQuery : IRequest<PaginationResult<Course>>
    {

        public PaginationCoursesQuery(int pageNo, int pageSize, QueryCourseModel queryParams)
        {
            PageNumber = pageNo;
            PageSize = pageSize;
            _queryParams = queryParams;
        }

        public int PageNumber { get; }

        public int PageSize { get; }

        private readonly QueryCourseModel _queryParams;

        public Expression<Func<Course, bool>> Predicator
        {
            get
            {
                Expression<Func<Course, bool>> exp = c => true;
                if (_queryParams.AuditStatus is not null)
                    exp = exp.And(c => c.AuditStatus == _queryParams.AuditStatus);

                if (_queryParams.PublishStatus is not null)
                    exp = exp.And(c => c.PublishStatus == _queryParams.PublishStatus);

                if (_queryParams.CourseName is not null)
                    exp = exp.And((Course c) => c.Name.Contains(_queryParams.CourseName));

                return exp;
            }
        }
    }
}
