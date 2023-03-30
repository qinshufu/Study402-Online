using MediatR;
using Microsoft.EntityFrameworkCore;
using Study402Online.DictionaryService.Api.Application.Request;
using Study402Online.DictionaryService.Api.Instructure;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Application.RequestHandlers
{
    public class QueryDictionaryHandler : IRequestHandler<QueryDictionaryRequest, DataDictionary>
    {
        private readonly DbSet<DataDictionary> _dictionaries;

        public QueryDictionaryHandler(DataDictionaryContext db)
        {
            _dictionaries = db.DataDictionaries;
        }

        public Task<DataDictionary> Handle(QueryDictionaryRequest request, CancellationToken cancellationToken) =>
            _dictionaries
            .Where(d => d.Code == request.Code)
            .SingleAsync(cancellationToken);
    }
}
