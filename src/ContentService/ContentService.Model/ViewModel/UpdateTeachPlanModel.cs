using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Model.ViewModel;

public class UpdateTeachPlanModel
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
    /// 所属课程发布
    /// </summary>
    public int? CoursePublish { get; set; }

    /// <summary>
    /// 课程计划是否存在
    /// </summary>
    public bool Exists { get; set; }

    /// <summary>
    /// 是否允许预览
    /// </summary>
    public bool? Preview { get; set; }
}
