using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Linq;
using Study402Online.Common.ViewModel;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.RequestHandlers
{
    public class PaginationCoursesHandler : IRequestHandler<PaginationCoursesRequest, PaginationResult<Course>>
    {
        private readonly DbSet<Course> _courses;

        public PaginationCoursesHandler(DbContext context)
        {
            _courses = context.Set<Course>();
        }

        public Task<PaginationResult<Course>> Handle(PaginationCoursesRequest request, CancellationToken cancellationToken)
            => _courses.Where(request.Predicator).OrderBy(c => c.CreateTime).PaginationAsync(request.PageNumber, request.PageSize);
    }
}
