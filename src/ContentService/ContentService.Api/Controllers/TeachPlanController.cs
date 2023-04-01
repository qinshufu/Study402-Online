using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.ContentService.Api.Application.Queries;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Controllers
{
    /// <summary>
    /// 课程计划
    /// </summary>
    [Route("api/teach-plan")]
    [ApiController]
    public class TeachPlanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachPlanController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// 获取课程计划（树状）
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpGet("tree")]
        public Task<List<TeachPlanTreeNode>> GetTeachPlans([FromQuery] int course)
        {
            var command = new TeachPlanTreeQuery(course);
            return _mediator.Send(command);
        }
    }
}
