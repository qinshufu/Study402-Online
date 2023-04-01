using System.ComponentModel.DataAnnotations.Schema;

namespace Study402Online.ContentService.Model.DataModel
{
    /// <summary>
    /// 课程营销信息
    /// </summary>
    public class CourseMarket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

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
}
