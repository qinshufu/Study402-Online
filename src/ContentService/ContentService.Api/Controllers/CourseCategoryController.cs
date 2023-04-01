using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Controllers
{
    /// <summary>
    /// 课程分类
    /// </summary>
    [Route("api/course-category")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 课程分类树形列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("tree-nodes")]
        public Task<CourseCategoriesTreeModel> Get([FromQuery] string id)
            => _mediator.Send(new CourseCategoriesQuery(id));
    }
}