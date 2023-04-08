namespace Study402Online.BuildingBlocks.LocalMessage;


/// <summary>
/// 本地消息选项
/// </summary>
public class LocalMessageOption
{
    /// <summary>
    /// 最大重试次数
    /// </summary>
    public int MaxRetriesNumber { get; internal set; }

    /// <summary>
    /// 预取消息数量
    /// </summary>
    public int PrefetchesNumber { get; internal set; }

    /// <summary>
    /// 并行数量
    /// </summary>
    public int ParallelismsNumber { get; internal set; }
}