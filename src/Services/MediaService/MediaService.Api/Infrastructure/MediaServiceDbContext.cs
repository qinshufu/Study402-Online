using Microsoft.EntityFrameworkCore;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Infrastructure;

public class MediaServiceDbContext : DbContext
{
    public DbSet<MediaFile> MediaFiles { get; set; }

    public DbSet<MediaProcess> MediaProcesses { get; set; }

    public DbSet<MediaProcessHistory> MediaProcessHistories { get; set; }

    public DbSet<FileBlock> FileBlocks { get; set; }

    public DbSet<ChunkUpload> ChunkUploads { get; set; }

    public MediaServiceDbContext(DbContextOptions options) : base(options)
    {
    }
}