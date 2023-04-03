using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Linq;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Queries
{
    public class PaginationCoursesQueryHandler : IRequestHandler<PaginationCoursesQuery, Result<PaginationResult<Course>>>
    {
        private readonly DbSet<Course> _courses;

        public PaginationCoursesQueryHandler(ContentDbContext context)
        {
            _courses = context.Set<Course>();
        }

        public async Task<Result<PaginationResult<Course>>> Handle(PaginationCoursesQuery request, CancellationToken cancellationToken)
        {
            var data = await _courses
                .Where(request.Predicator)
                .OrderBy(c => c.CreateTime)
                .PaginationAsync(request.PageNumber, request.PageSize);
            return ResultFactory.Success(data);
        }
    }
}
