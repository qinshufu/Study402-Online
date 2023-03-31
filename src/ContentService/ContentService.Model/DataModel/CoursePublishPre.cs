namespace Study402Online.ContentService.Model.DataModel
{
    /// <summary>
    /// 课程预发布表
    /// </summary>
    public class CoursePublishPre
    {
        public int Id { get; set; }

        /// <summary>
        /// 公司 ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

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
        /// 分类
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        public string SubClass { get; set; }

        /// <summary>
        /// 子分类名称
        /// </summary>
        public string SubClassName { get; set; }

        /// <summary>
        /// 课程等级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 教育模式
        /// </summary>
        public int TeachMode { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public Uri Picture { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 课程营销信息， JSON
        /// </summary>
        public string Market { get; set; }

        /// <summary>
        /// 课程计划，JSON
        /// </summary>
        public string TeachPlan { get; set; }

        /// <summary>
        /// 教师信息，JSON
        /// </summary>
        public string Teachers { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 收费规则，对应数据字典--203
        /// </summary>
        public string Charge { get; set; }

        /// <summary>
        /// 现价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 课程有效期（天）
        /// </summary>
        public int ValidDays { get; set; }
    }
}
