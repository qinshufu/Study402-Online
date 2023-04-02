using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Application.Commands;
using Study402Online.ContentService.Api.Application.Queries;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

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
        public Task<Result<List<TeachPlanTreeNode>>> GetTeachPlans([FromQuery] int course)
        {
            var command = new TeachPlanTreeQuery(course);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 添加课程计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public Task<Result<TeachPlan>> AddTeachPlan([FromBody] AddTeachPlanModel model)
        {
            var command = new AddTeachPlanCommand(model);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 更新课程计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public Task<Result<TeachPlan>> UpdateTeachPlan([FromBody] UpdateTeachPlanModel model)
        {
            var command = new UpdateTeachPlanCommand(model);
            return _mediator.Send(command);
        }

        /// <summary>
        /// 删除课程计划
        /// </summary>
        /// <param name="teachPlan"></param>
        /// <returns></returns>
        [HttpDelete]
        public Task<Result<int>> DeleteTeachPlan([FromQuery] int teachPlan)
        {
            var command = new DeleteTeachPlanCommand(teachPlan);
            return _mediator.Send(command);
        }
    }
}
