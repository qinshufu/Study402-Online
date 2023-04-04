namespace Study402Online.MediaService.Model.DataModel;

/// <summary>
/// 文件块表
/// </summary>
public class FileBlock
{
    public Guid Id { get; set; }

    public string BlockHash { get; set; }

    public string FileHash { get; set; }

    public string ObjectKey { get; set; }
}
