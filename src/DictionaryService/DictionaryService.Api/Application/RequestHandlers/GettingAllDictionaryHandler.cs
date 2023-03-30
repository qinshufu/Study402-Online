using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.DictionaryService.Api.Application.Request;
using Study402Online.DictionaryService.Api.Instructure;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Application.RequestHandlers
{
    public class GettingAllDictionaryHandler : IRequestHandler<GettingAllDictionaryRequest, List<DataDictionary>>
    {
        private readonly DbSet<DataDictionary> _dictionaries;

        public GettingAllDictionaryHandler(DataDictionaryContext context)
        {
            _dictionaries = context.DataDictionaries;
        }

        public Task<List<DataDictionary>> Handle(GettingAllDictionaryRequest request, CancellationToken cancellationToken)
            => _dictionaries.ToListAsync(cancellationToken);
    }
}
