using Microsoft.EntityFrameworkCore;
using Study402Online.OrderService.Model.DateModels;

namespace Study402Online.OrderService.Api.Instructure;

/// <summary>
/// 订单服务数据库上级文
/// </summary>
public class OrderServiceDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderPayRecord> OrderPayRecords { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options) : base(options)
    {
    }
}
