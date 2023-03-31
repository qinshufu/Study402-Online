namespace Study402Online.ContentService.Model.DataModel
{
    /// <summary>
    /// 课程发布信息
    /// </summary>
    public class CoursePublish
    {
        public int Id { get; set; }

        /// <summary>
        /// 公司 ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 适用用户
        /// </summary>
        public string Users { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? Creater { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 分类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        public string SubClass { get; set; }

        /// <summary>
        /// 子分类名
        /// </summary>
        public string SubClassName { get; set; }

        /// <summary>
        /// 课程等级
        /// </summary>
        public int CourseGrade { get; set; }

        /// <summary>
        /// 教育模式
        /// </summary>
        public int TeachMode { get; set; }

        /// <summary>
        /// 课程图片
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 课程介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 课程营销信息，JSON 格式
        /// </summary>
        public string Market { get; set; }

        /// <summary>
        /// 课程计划, JSON 格式
        /// </summary>
        public string TeachPlan { get; set; }

        /// <summary>
        /// 教师信息，JSON 格式
        /// </summary>
        public string? Teachers { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? OnlineTime { get; set; }

        /// <summary>
        /// 下架时间
        /// </summary>
        public DateTime? OfflineTime { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public int PublishStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 收费规则，对应数据字典 - 203
        /// </summary>
        public string Charge { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 课程有效期，天数
        /// </summary>
        public int ValidDays { get; set; }
    }
}
