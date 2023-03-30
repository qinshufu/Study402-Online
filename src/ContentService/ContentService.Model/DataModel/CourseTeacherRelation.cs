namespace Study402Online.ContentService.Model.DataModel
{
    /// <summary>
    /// 课程-教师关系
    /// </summary>
    public class CourseTeacherRelation
    {
        public int Id { get; set; }

        /// <summary>
        /// 课程 ID
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 教师职位
        /// </summary>
        public string TeacherPosition { get; set; }

        /// <summary>
        /// 教师简介
        /// </summary>
        public string TeacherIntroduction { get; set; }

        /// <summary>
        /// 教师照片
        /// </summary>
        public string TeacherPhotograph { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
