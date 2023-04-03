using Study402Online.ContentService.Model.DataModel;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Study402Online.ContentService.Model.ViewModel
{
    public class CourseCategoriesTreeModel
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

        /// <summary>
        /// 子分类集合
        /// </summary>
        public List<CourseCategoriesTreeModel> Children { get; set; } = new List<CourseCategoriesTreeModel>(0);

        public static CourseCategoriesTreeModel Create(CourseCategory categroy, List<CourseCategoriesTreeModel> children)
            => new CourseCategoriesTreeModel
            {
                Id = categroy.Id,
                Name = categroy.Name,
                Label = categroy.Label,
                Parent = categroy.Parent,
                IsShow = categroy.IsShow,
                OrderBy = categroy.OrderBy,
                IsSub = categroy.IsSub,
                Children = children,

            };

    }
}