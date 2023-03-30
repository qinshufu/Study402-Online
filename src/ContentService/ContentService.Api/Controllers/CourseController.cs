using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.ViewModel;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpPost("list")]
        public PaginationResult<Course> Pagination(
            [FromBody] QueryCourseModel queryParams,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            return new PaginationResult<Course>
            {
                Items = new List<Course>(),
                Counts = 0,
                Page = pageNo,
                PageSize = pageSize
            };
        }
    }
}
