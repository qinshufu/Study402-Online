using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Linq;
using Study402Online.Common.ViewModel;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Queries
{
    public class PaginationCoursesQueryHandler : IRequestHandler<PaginationCoursesQuery, PaginationResult<Course>>
    {
        private readonly DbSet<Course> _courses;

        public PaginationCoursesQueryHandler(ContentDbContext context)
        {
            _courses = context.Set<Course>();
        }

        public Task<PaginationResult<Course>> Handle(PaginationCoursesQuery request, CancellationToken cancellationToken)
            => _courses.Where(request.Predicator).OrderBy(c => c.CreateTime).PaginationAsync(request.PageNumber, request.PageSize);
    }
}
