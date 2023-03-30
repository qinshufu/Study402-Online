using System.Linq.Expressions;

namespace Study402Online.Common.Expressions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var param = Expression.Parameter(typeof(T), "p" + Random.Shared.Next());
            var body = Expression.And(Expression.Invoke(left, param), Expression.Invoke(right, param));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
