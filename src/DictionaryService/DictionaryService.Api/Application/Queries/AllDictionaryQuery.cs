using MediatR;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Application.Request
{
    public class AllDictionaryQuery : IRequest<List<DataDictionary>>
    {

    }
}
