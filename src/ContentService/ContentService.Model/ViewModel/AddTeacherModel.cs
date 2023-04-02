namespace Study402Online.Study402Online.ContentService.Model.ViewModel;

public class AddTeacherModel
{
    /// <summary>
    /// 课程
    /// </summary>
    public int Course { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string Position { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Introduction { get; set; }

    /// <summary>
    /// 照片
    /// </summary>
    public Uri Photograph { get; set; }
}
