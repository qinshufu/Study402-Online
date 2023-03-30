using Microsoft.EntityFrameworkCore;
using Study402Online.Common.ViewModel;

namespace Study402Online.Common.Linq
{
    public static class PaginationOperator
    {
        public static PaginationResult<T> Pagination<T>(this IOrderedEnumerable<T> values, int pageNo, int pageSize)
            where T : class
        {
            var items = values.Skip((pageNo - 1) * pageSize).Take(pageSize).ToArray();
            var count = values.Count();

            return new PaginationResult<T> { Items = items, Counts = count, Page = pageNo, PageSize = 0 };
        }

        public static PaginationResult<T> Pagination<T>(this IOrderedQueryable<T> values, int pageNo, int pageSize)
            where T : class
        {
            var items = values.Skip((pageNo - 1) * pageSize).Take(pageSize).ToArray();
            var count = values.Count();

            return new PaginationResult<T> { Items = items, Counts = count, Page = pageNo, PageSize = 0 };
        }

        public static async Task<PaginationResult<T>> PaginationAsync<T>(this IOrderedEnumerable<T> values, int pageNo, int pageSize)
            where T : class
        {
            await Task.Yield();

            var items = values.Skip((pageNo - 1) * pageSize).Take(pageSize).ToArray();
            var count = values.Count();

            return new PaginationResult<T> { Items = items, Counts = count, Page = pageNo, PageSize = 0 };
        }

        public static async Task<PaginationResult<T>> PaginationAsync<T>(this IOrderedQueryable<T> values, int pageNo, int pageSize)
            where T : class
        {
            var items = await values.Skip((pageNo - 1) * pageSize).Take(pageSize).ToArrayAsync();
            var count = values.Count();

            return new PaginationResult<T> { Items = items, Counts = count, Page = pageNo, PageSize = 0 };
        }
    }
}
