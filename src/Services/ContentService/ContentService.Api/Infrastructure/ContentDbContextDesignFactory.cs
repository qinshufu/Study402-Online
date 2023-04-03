using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Study402Online.ContentService.Api.Infrastructure
{
    public class ContentDbContextDesignFactory : IDesignTimeDbContextFactory<ContentDbContext>
    {
        public ContentDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ContentDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Study402Online.ContentService;Integrated Security=True;Connect Timeout=30;");

            return new ContentDbContext(builder.Options);
        }
    }
}
