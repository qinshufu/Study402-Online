using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.DictionaryService.Api.Application.Request;
using Study402Online.DictionaryService.Api.Instructure;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Application.Queries
{
    public class AllDictionaryQueryHandler : IRequestHandler<AllDictionaryQuery, List<DataDictionary>>
    {
        private readonly DbSet<DataDictionary> _dictionaries;

        public AllDictionaryQueryHandler(DataDictionaryContext context)
        {
            _dictionaries = context.DataDictionaries;
        }

        public Task<List<DataDictionary>> Handle(AllDictionaryQuery request, CancellationToken cancellationToken)
            => _dictionaries.ToListAsync(cancellationToken);
    }
}
