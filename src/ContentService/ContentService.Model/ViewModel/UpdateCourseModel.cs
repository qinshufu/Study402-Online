namespace Study402Online.Study402Online.ContentService.Model.ViewModel;

public class UpdateCourseModel
{
    public int Id { get; set; }

    /// <summary>
    /// 课程名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 适用人群
    /// </summary>
    public string? Users { get; set; }

    /// <summary>
    /// 课程标签
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// 大分类
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// 小分类
    /// </summary>
    public string? SubClass { get; set; }

    /// <summary>
    /// 课程介绍
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 课程等级
    /// </summary>
    public string Grade { get; set; }

    /// <summary>
    /// 教育模式
    /// </summary>
    public string TeachMode { get; set; }

    /// <summary>
    /// 课程图片
    /// </summary>
    public Uri Picture { get; set; }

    /// <summary>
    /// 收费规则，对应数据字段
    /// </summary>
    public string Chargeting { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public Decimal Price { get; set; }

    /// <summary>
    /// 原价
    /// </summary>
    public Decimal OriginalPrice { get; set; }

    /// <summary>
    /// QQ
    /// </summary>
    public string? QQ { get; set; }

    /// <summary>
    /// 微信
    /// </summary>
    public string? Wechat { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 有效期天数
    /// </summary>
    public int? ValidDays { get; set; }
}
