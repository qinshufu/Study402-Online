using Microsoft.EntityFrameworkCore;
using Study402Online.BuildingBlocks.LocalMessage.Storage;

namespace LocalMessage.Test;

/// <summary>
/// 数据库工具类，用于设置测试数据库
/// </summary>
public static class DbHelper
{
    const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=LocalMessage.Test;Trusted_Connection=True";

    private static readonly ManualResetEvent _event = new(true);

    /// <summary>
    /// 创建 DbContext，将会将原来对应的数据清除
    /// </summary>
    /// <returns></returns>
    public static MessageLogDbContext CreateDbContext()
    {
        var dbContext = new MessageLogDbContext(new DbContextOptionsBuilder<MessageLogDbContext>().UseSqlServer(ConnectionString).Options);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        return dbContext;
    }

    /// <summary>
    /// 获取锁
    /// </summary>
    /// <remarks>
    /// 如果测试中有 async 方法的话，测试将并行执行，然而数据库不能够并行，因为每次测试前都应该先清理上一个测试留下来的数据，因此数据库成为了竞态条件。
    /// 所以需要锁同步。
    /// </remarks>
    public static void Aquire() => _event.WaitOne();

    /// <summary>
    /// 释放锁
    /// </summary>
    /// <remarks>
    /// 释放数据库的锁，对应的获取操作为 <see cref="Aquire"/>。
    /// </remarks>
    public static void Release() => _event.Set();
}
