namespace Study402Online.StudyService.Api.Models.DataModels;

/// <summary>
/// 课程表
/// </summary>
public class ClassSchedule
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 选课记录 ID
    /// </summary>
    public int CourseSelectionId { get; set; }

    /// <summary>
    /// 用户 ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 公司 ID
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// 课程 ID
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// 课程名
    /// </summary>
    public string CourseName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 开始生效时间
    /// </summary>
    public DateTime ToTakeEffectTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime ExpirationTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 注释
    /// </summary>
    public string Remarks { get; set; }
}
