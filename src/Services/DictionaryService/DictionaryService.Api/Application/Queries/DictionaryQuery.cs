using MediatR;
using Study402Online.Common.Model;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Application.Request
{
    public class DictionaryQuery : IRequest<Result<DataDictionary>>
    {
        private readonly string _code;

        public string Code => _code;

        public DictionaryQuery(string code)
        {
            _code = code;
        }
    }
}
