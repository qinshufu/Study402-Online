using MediatR;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Application.Request
{
    public class QueryDictionaryRequest : IRequest<DataDictionary>
    {
        private readonly string _code;

        public string Code => _code;

        public QueryDictionaryRequest(string code)
        {
            _code = code;
        }
    }
}
