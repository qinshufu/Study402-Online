namespace Study402Online.ContentService.Model.ViewModel;

/// <summary>
/// 绑定媒体模型
/// </summary>
public class BindMediaModel
{
    /// <summary>
    /// 媒资文件 ID
    /// </summary>
    public string MediaFile { get; set; }

    /// <summary>
    /// 媒资文件名
    /// </summary>
    public string MediaName { get; set; }

    /// <summary>
    /// 课程计划 ID
    /// </summary>
    public int TeachPlan { get; set; }
}