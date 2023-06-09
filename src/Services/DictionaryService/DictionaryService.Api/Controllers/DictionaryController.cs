using MediatR;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.DictionaryService.Api.Application.Request;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DictionaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public Task<Result<List<DataDictionary>>> All() => _mediator.Send(new AllDictionaryQuery());

        [HttpGet("code")]
        public Task<Result<DataDictionary>> GetByCode([FromQuery] string code) => _mediator.Send(new DictionaryQuery(code));
    }
}
