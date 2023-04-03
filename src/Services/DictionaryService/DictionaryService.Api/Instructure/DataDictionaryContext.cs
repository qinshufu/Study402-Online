using Microsoft.EntityFrameworkCore;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Instructure
{
    public class DataDictionaryContext : DbContext
    {
        public DbSet<DataDictionary> DataDictionaries { get; set; }

        public DataDictionaryContext(DbContextOptions<DataDictionaryContext> options) : base(options) { }
    }
}
