using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.ViewModel;
using Study402Online.Common.Expressions;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;
using System.Linq.Expressions;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Controllers
{
    /// <summary>
    /// 课程相关
    /// </summary>
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 课程列表（分页）
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("list")]
        public Task<PaginationResult<Course>> Pagination(
            [FromBody] QueryCourseModel queryParams,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            Expression<Func<Course, bool>> exp = c => true;

            if (queryParams.AuditStatus is not null)
                exp = exp.And(c => c.AuditStatus == queryParams.AuditStatus);

            if (queryParams.PublishStatus is not null)
                exp = exp.And(c => c.PublishStatus == queryParams.PublishStatus);

            if (queryParams.CourseName is not null)
                exp = exp.And((Course c) => c.Name.Contains(queryParams.CourseName));

            var request = new PaginationCoursesRequest(pageNo, pageSize, exp);
            return _mediator.Send(request);
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public Task<AddCourseResultModel> AddCourse([FromBody] AddCourseModel model)
        {
            var command = new AddCourseCommand(model);
            return _mediator.Send(command);
        }
    }
}
