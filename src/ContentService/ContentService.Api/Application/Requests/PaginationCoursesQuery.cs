using MediatR;
using Study402Online.Common.ViewModel;
using Study402Online.ContentService.Model.DataModel;
using System.Linq.Expressions;

namespace Study402Online.ContentService.Api.Application.Requests
{
    public class PaginationCoursesQuery : IRequest<PaginationResult<Course>>
    {
        public PaginationCoursesQuery(int pageNumber, int pageSize, Expression<Func<Course, bool>> predicator)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Predicator = predicator;
        }

        public int PageNumber { get; }

        public int PageSize { get; }

        public Expression<Func<Course, bool>> Predicator { get; }
    }
}
