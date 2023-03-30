namespace Study402Online.ContentService.Model.DataModel
{
    /// <summary>
    /// 课程计划媒体文件
    /// </summary>
    public class TeachPlanMedia
    {
        public int Id { get; set; }

        /// <summary>
        /// 媒体文件 ID
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 课程计划
        /// </summary>
        public int TeachPlan { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creater { get; set; }
    }
}
