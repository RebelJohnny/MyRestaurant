using MyRestaurant.Framework.Querying.Sorts;
using System.Linq.Expressions;
using System.Reflection;

namespace MyRestaurant.Framework.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return pageIndex < 0
                ? query
                : query.Skip(pageIndex * pageSize).Take(pageSize);
        }
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IEnumerable<SortParams> sorts, string? defaultSortField = null)
        {
            var sortList = sorts.ToList();
            if (sortList.Count == 0)
            {
                if (string.IsNullOrEmpty(defaultSortField))
                {
                    return query;
                }
                else
                {
                    sortList.Add(new SortParams
                    {
                        Field = defaultSortField,
                        IsDescending = true
                    });
                }
            }
            Expression finalResult = query.Expression;

            bool isFirst = true;
            for (int i = 0; i < sortList.Count; i++)
            {
                var sort = sortList[i];
                if (string.IsNullOrWhiteSpace(sort.Field))
                {
                    continue;
                }
                PropertyInfo? property = typeof(T).GetProperty(sort.Field, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property is null)
                {
                    continue;
                }
                Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression body = Expression.Property(parameter, property);
                LambdaExpression lambda = Expression.Lambda(body, parameter);

                string methodName;

                if (isFirst)
                {
                    methodName = sort.IsDescending
                        ? nameof(Queryable.OrderByDescending)
                        : nameof(Queryable.OrderBy);
                }
                else
                {
                    methodName = sort.IsDescending
                        ? nameof(Queryable.ThenByDescending)
                        : nameof(Queryable.ThenBy);
                }

                finalResult = Expression.Call(typeof(Queryable), methodName, [typeof(T), type], finalResult, Expression.Quote(lambda));
                isFirst = false;
            }

            return query.Provider.CreateQuery<T>(finalResult);
        }
    }
}
