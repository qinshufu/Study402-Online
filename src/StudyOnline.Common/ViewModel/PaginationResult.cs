namespace Study402Online.Common.ViewModel;

public class PaginationResult<T>
{
    /// <summary>
    /// 数据列表
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new T[0];

    /// <summary>
    /// 总记录数
    /// </summary>
    public int Counts { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// 每页记录数
    /// </summary>
    public int PageSize { get; set; }
}
