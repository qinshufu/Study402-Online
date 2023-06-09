using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;
using Study402Online.ContentService.Model.ViewModel;
using Study402Online.ContentService.Api.Application.Queries;
using Study402Online.ContentService.Api.Application.Commands;
using Study402Online.Common.Linq;
using Study402Online.Common.Model;

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
        public Task<Result<PaginationResult<Course>>> Pagination(
            [FromBody] QueryCourseModel queryParams,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var request = new PaginationCoursesQuery(pageNo, pageSize, queryParams);
            return _mediator.Send(request);
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public Task<Result<CourseInformationModel>> AddCourse([FromBody] AddCourseModel model)
        {
            var command = new AddCourseCommand(model);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 获取课程信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public Task<Result<CourseInformationModel>> GetCourse([FromQuery] int id)
        {
            var command = new CourseQuery(id);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 修改课程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public Task<Result<CourseInformationModel>> UpdateCourse([FromBody] UpdateCourseModel model)
        {
            var command = new UpdateCourseCommand(model);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public Task<Result<object>> DeleteCourse([FromQuery] int course)
        {
            var command = new DeleteCourseCommand(course);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 发布课程
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost("publish/{course:int}")]
        public Task<UnitResult> PublishCourse([FromRoute(Name = "course")] int course)
        {
            var command = new PublishCourseCommand(course);
            return _mediator.Send(command);
        }
    }
}
