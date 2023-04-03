using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Study402Online.DictionaryService.Model.DataModel;

namespace Study402Online.DictionaryService.Api.Instructure
{
    public class DataDictionaryDesignFactory : IDesignTimeDbContextFactory<DataDictionaryContext>
    {
        public DataDictionaryContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataDictionaryContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Study402Online.DictionaryService;Integrated Security=True;Connect Timeout=30;");

            return new DataDictionaryContext(builder.Options);
        }
    }
}
