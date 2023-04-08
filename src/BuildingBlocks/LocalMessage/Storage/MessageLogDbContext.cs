using Microsoft.EntityFrameworkCore;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;

namespace Study402Online.BuildingBlocks.LocalMessage.Storage;

/// <summary>
/// 本地消息数据库上下文
/// </summary>
public class MessageLogDbContext : DbContext
{
    public DbSet<MessageLog> MessageLogs { get; set; }

    public DbSet<MessageHistory> MessageHistories { get; set; }

    public MessageLogDbContext(DbContextOptions<MessageLogDbContext> options) : base(options)
    {

    }
}