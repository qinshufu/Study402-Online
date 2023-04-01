using Dapper;
using MediatR;
using Study402Online.ContentService.Api.Application.Requests;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Study402Online.ContentService.Api.Application.RequestHandlers
{
    public class CourseCategoriesQueryHandler : IRequestHandler<CourseCategoriesQuery, CourseCategoriesTreeModel>
    {
        private readonly DbConnection _connection;

        public CourseCategoriesQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CourseCategoriesTreeModel> Handle(CourseCategoriesQuery request, CancellationToken cancellationToken)
        {
            // 递归查询
            var sql = @"
with CourseCategoriesTreeQuery as (
    select t1.Id, t1.Name, t1.Label, t1.Parent, t1.IsShow, t1.OrderBy, t1.IsSub from dbo.Categories as t1 where Id = @id
    union all
    select t2.Id, t2.Name, t2.Label, t2.Parent, t2.IsShow, t2.OrderBy, t2.IsSub from dbo.Categories as t2
    join CourseCategoriesTreeQuery as tree on t2.Id like tree.Id+'-'+'%'
)

select * from CourseCategoriesTreeQuery;";
            var categories = await _connection.QueryAsync<CourseCategory>(sql, new { id = request.Id });

            if (categories.Any() is false)
                throw new InvalidOperationException("指定课程分类不存在: " + request.Id);

            var root = categories.Single(c => c.Id == request.Id);
            var children = GetCategorityNodeChildren(root, categories, cancellationToken).ToList();
            return CourseCategoriesTreeModel.Create(root, children);
        }

        private IEnumerable<CourseCategoriesTreeModel> GetCategorityNodeChildren(
            CourseCategory root, IEnumerable<CourseCategory> categories, CancellationToken? cancellationToken = null)
        {
            static bool IsChild(string parent, string other) => Regex.Match(other, $@"{parent}-\d+").Success;

            cancellationToken?.ThrowIfCancellationRequested();

            categories = categories.ToArray();

            var children = from c in categories.Where(c => IsChild(root.Id, c.Id))
                           select CourseCategoriesTreeModel.Create(c, GetCategorityNodeChildren(c, categories, cancellationToken).ToList());

            return children;
        }
    }
}