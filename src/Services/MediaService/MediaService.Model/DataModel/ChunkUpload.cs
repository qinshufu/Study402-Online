namespace Study402Online.MediaService.Model.DataModel;

/// <summary>
/// 分片上传记录表
/// </summary>
public class ChunkUpload
{
    public int Id { get; set; }

    /// <summary>
    /// 文件 Hash
    /// </summary>
    public string FileHash { get; set; }

    /// <summary>
    /// 上传 ID
    /// </summary>
    public string UploadId { get; set; }

    /// <summary>
    /// 上传状态
    /// </summary>
    public ChunkUploadStatus Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

public enum ChunkUploadStatus
{
    /// <summary>
    /// 进行中
    /// </summary>
    Pending,

    /// <summary>
    /// 成功
    /// </summary>
    Complete,

    /// <summary>
    /// 失败
    /// </summary>
    Fail
}