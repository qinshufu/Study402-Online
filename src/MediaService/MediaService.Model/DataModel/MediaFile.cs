namespace Study402Online.MediaService.Model.DataModel;

/// <summary>
/// 媒体文件
/// </summary>
public class MediaFile
{
    /// <summary>
    /// 文件 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 机构
    /// </summary>
    public int Organization { get; set; }

    /// <summary>
    /// 机构名
    /// </summary>
    public string OrganizationName { get; set; }

    /// <summary>
    /// 文件 ID
    /// </summary>
    public string FileId { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// 存储路径
    /// </summary>
    public string StoragePath { get; set; }

    /// <summary>
    /// 访问路径
    /// </summary>
    public string AccessPath { get; set; }

    /// <summary>
    /// 上传者
    /// </summary>
    public string Updater { get; set; }

    /// <summary>
    /// 上传时间
    /// </summary>
    public DateTime UploadTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remarks { get; set; }

    /// <summary>
    /// 审核状态
    /// </summary>
    public string AuditStatus { get; set; }

    /// <summary>
    /// 审核意见
    /// </summary>
    public string AuditOpinion { get; set; }
}