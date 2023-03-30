namespace Study402Online.ContentService.Model.DataModel
{
    public class Course
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public required string CompanyName { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public required string Name { get; set; }

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
        public string Description { get; set; }

        /// <summary>
        /// 课程图片
        /// </summary>
        public Uri Picture { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creater { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 课程发布状态
        /// </summary>
        public PublishStatus PublishStatus { get; set; }
    }
}
