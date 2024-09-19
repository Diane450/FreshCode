using System.Globalization;
using System.Linq.Expressions;

namespace FreshCode.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string sortBy, bool descending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = typeof(T).GetProperty(sortBy);
            if (property == null)
            {
                throw new ArgumentException($"Property '{sortBy}' does not exist on type '{typeof(T)}'.", nameof(sortBy));
            }

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = descending ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, string filterBy, string filterValue)
        {
            if (string.IsNullOrWhiteSpace(filterBy) || string.IsNullOrWhiteSpace(filterValue))
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = typeof(T).GetProperty(filterBy);
            if (property == null)
            {
                throw new ArgumentException($"Property '{filterBy}' does not exist on type '{typeof(T)}'.", nameof(filterBy));
            }

            var constantValue = Expression.Constant(filterValue);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var equalExpression = Expression.Equal(propertyAccess, constantValue);

            var whereExpression = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

            var resultExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { typeof(T) },
                query.Expression,
                Expression.Quote(whereExpression));

            return query.Provider.CreateQuery<T>(resultExpression);

        }
    }
}
