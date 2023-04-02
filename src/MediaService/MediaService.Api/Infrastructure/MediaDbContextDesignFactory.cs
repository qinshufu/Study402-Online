using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Study402Online.MediaService.Api.Infrastructure
{
    public class MediaDbContextDesignFactory : IDesignTimeDbContextFactory<MediaServiceDbContext>
    {
        MediaServiceDbContext IDesignTimeDbContextFactory<MediaServiceDbContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MediaServiceDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Study402Online.MediaService;Integrated Security=True;Connect Timeout=30;");
            return new MediaServiceDbContext(builder.Options);
        }
    }
}
