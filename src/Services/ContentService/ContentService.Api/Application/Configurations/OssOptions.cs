namespace Study402Online.ContentService.Api.Application.Configurations;

public record OssOptions
{
    /// <summary>
    /// 文件桶
    /// </summary>
    public string FileBucket { get; init; }

    /// <summary>
    /// 大文件桶
    /// </summary>
    public string BigFileBucket { get; init; }

    /// <summary>
    /// 端点
    /// </summary>
    public string Endpoint { get; init; }

    /// <summary>
    /// 访问 key
    /// </summary>
    public string AccessKey { get; init; }

    /// <summary>
    /// 访问 secret
    /// </summary>
    public string AccessKeySecret { get; init; }
}
