using Study402Online.ContentService.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study402Online.ContentService.Model.ViewModel;

/// <summary>
/// 课程计划信息
/// </summary>
public class TeachPlanInfo
{
    public int Id { get; set; }

    /// <summary>
    /// 课程计划名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 课程计划父级
    /// </summary>
    public int Parent { get; set; }

    /// <summary>
    /// 课程计划层级
    /// </summary>
    public TeachPlanLevel Level { get; set; }

    /// <summary>
    /// 课程计划媒体类型
    /// </summary>
    public TeachPlanMediaType? Type { get; set; }

    /// <summary>
    /// 开始直播时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束直播时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 课程描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 直播时长 (分钟)
    /// </summary>
    public float? Duration { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderBy { get; set; }

    /// <summary>
    /// 所属课程
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// 是否允许预览
    /// </summary>
    public bool? Preview { get; set; }

    /// <summary>
    /// 媒体文件 ID
    /// </summary>
    public string MediaId { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string MediaName { get; set; }
}
