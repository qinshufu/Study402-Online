using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.ViewModel;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;
using System.Linq.Expressions;

namespace Study402Online.ContentService.Api.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        public Task<PaginationResult<Course>> Pagination(
            [FromBody] QueryCourseModel queryParams,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            dynamic expBody = Expression.Constant(true);
            var expParams = Expression.Parameter(typeof(string), "course");

            if (queryParams.CourseName is not null)
            {
                expBody = Expression.AndAlso(expBody,
                    Expression.Equal(Expression.Constant(queryParams.CourseName), Expression.Property(expParams, nameof(Course.Name))));
            }

            if (queryParams.PublishStatus is not null)
            {
                expBody = Expression.AndAlso(expBody,
                    Expression.Equal(Expression.Constant(queryParams.PublishStatus), Expression.Property(expParams, nameof(Course.PublishStatus))));
            }

            if (queryParams.AuditStatus is not null)
            {
                expBody = Expression.AndAlso(expBody,
                    Expression.Equal(Expression.Constant(queryParams.AuditStatus), Expression.Property(expParams, nameof(Course.AuditStatus))));
            }

            var exp = Expression.Lambda(expBody, expParams);

            var request = new PaginationCoursesRequest(pageNo, pageSize, exp);
            return _mediator.Send(request);
        }
    }
}
