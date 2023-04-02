namespace Study402Online.MediaService.Model.DataModel;

public class MediaProcessHistory
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文件ID
    /// </summary>
    public string FileId { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 存储桶
    /// </summary>
    public string StorageBucket { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 上传时间
    /// </summary>
    public DateTime UploadTime { get; set; }

    /// <summary>
    /// 完成时间
    /// </summary>
    public DateTime CompletionTime { get; set; }

    /// <summary>
    /// 访问路径
    /// </summary>
    public string AccessPath { get; set; }

    /// <summary>
    /// 存储路径
    /// </summary>
    public string StoragePath { get; set; }

    /// <summary>
    /// 失败信息
    /// </summary>
    public string FailureMessage { get; set; }
}