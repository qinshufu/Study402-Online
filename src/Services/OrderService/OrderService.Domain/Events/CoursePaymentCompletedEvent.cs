using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Events;


/// <summary>
/// 课程支付完成事件
/// </summary>
public record CoursePaymentCompletedEvent
{
    /// <summary>
    /// 是否支付成功
    /// </summary>
    public bool IsPaymentSuccessful { get; init; }

    /// <summary>
    /// 课程 ID
    /// </summary>
    public string CourseSelectionId { get; init; }
}
