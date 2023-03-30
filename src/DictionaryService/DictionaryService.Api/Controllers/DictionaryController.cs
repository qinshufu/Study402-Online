using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public Task<List<DataDictionary>> All() => _mediator.Send(new GettingAllDictionaryRequest());

        [HttpGet("code")]
        public Task<DataDictionary> GetByCode([FromQuery] string code) => _mediator.Send(new QueryDictionaryRequest(code));
    }
}
