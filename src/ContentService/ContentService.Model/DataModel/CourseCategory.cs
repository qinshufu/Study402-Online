namespace Study402Online.ContentService.Model.DataModel
{
    /// <summary>
    /// 课程分类
    /// </summary>
    public class CourseCategory
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// 标签，默认为分类名称
        /// </summary>
        public required string Label { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        public string? Parent { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderBy { get; set; }

        /// <summary>
        /// 是否子分类
        /// </summary>
        public bool IsSub { get; set; }
    }
}
