namespace Study402Online.StudyService.Api.Models.DataModels;

/// <summary>
/// 选课记录
/// </summary>
public class CourseSelectionRecord
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 课程名
    /// </summary>
    public string CourseName { get; set; }

    /// <summary>
    /// 课程 ID
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户 ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 机构 ID
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// 是否为付费课程
    /// </summary>
    public bool IsPaidCourse { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 有效时间，以天为单位
    /// </summary>
    public int ValidDays { get; set; }

    /// <summary>
    /// 选课状态
    /// </summary>
    public CourseSelectionStatus Status { get; set; }

    /// <summary>
    /// 开始生效时间
    /// </summary>
    public DateTime ToTakeEffectTime { get; set; }

    /// <summary>
    /// 选课失效时间
    /// </summary>
    public DateTime ExpirationTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remarks { get; set; }
}

/// <summary>
/// 课程选课状态
/// </summary>
public enum CourseSelectionStatus
{
    /// <summary>
    /// 待支付
    /// </summary>
    Unpaid,

    /// <summary>
    /// 选课成功
    /// </summary>
    Successful,

    /// <summary>
    /// 选课删除
    /// </summary>
    Removed
}