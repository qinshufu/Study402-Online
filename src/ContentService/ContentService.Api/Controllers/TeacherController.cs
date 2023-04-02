using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Application.Commands;
using Study402Online.Study402Online.ContentService.Model.DataModel;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Controllers
{
    /// <summary>
    /// 教师功能
    /// </summary>
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator)
            => _mediator = mediator;

        /// <summary>
        /// 添加教师
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public Task<Result<CourseTeacher>> AddTeacher([FromBody] AddTeacherModel teacher)
        {
            var command = new AddTeacherCommand(teacher);
            return _mediator.Send(command);
        }
    }
}
