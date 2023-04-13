using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study402Online.OrderService.Model.ViewModels;

/// <summary>
/// 订单项目
/// </summary>
public class OrderItemModel
{
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
