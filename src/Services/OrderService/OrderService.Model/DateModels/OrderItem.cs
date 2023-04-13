using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study402Online.OrderService.Model.DateModels;

/// <summary>
/// 订单项目
/// </summary>
public class OrderItem
{
    public int Id { get; set; }

    /// <summary>
    /// 订单 ID
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// 商品 ID
    /// </summary>
    public int GoodsId { get; set; }

    /// <summary>
    /// 商品类型
    /// </summary>
    public int GoodsType { get; set; }

    /// <summary>
    /// 商品名称
    /// </summary>
    public string GoodsName { get; set; }

    /// <summary>
    /// 商品价格
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 商品详情
    /// </summary>
    public string? Details { get; set; }
}
