using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;

namespace MyRestaurant.Framework.Querying.Filters
{
    public class PredicateBuilder<T> : IPredicateBuilder<T> where T : class
    {
        private static Expression<Func<T, bool>> CombineWithAnd(IEnumerable<Expression<Func<T, bool>>> predicates)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            if (predicates == null)
            {
                var trueExpression = Expression.Constant(true);
                return Expression.Lambda<Func<T, bool>>(trueExpression, parameter);
            }
            var combined = predicates
                .Select(p => ReplaceParameter(p.Body, p.Parameters[0], parameter))
                .Aggregate(Expression.AndAlso);

            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        private static Expression ReplaceParameter(Expression expression, ParameterExpression source, ParameterExpression target)
        {
            return new ParameterReplacer(source, target).Visit(expression);
        }

        private class ParameterReplacer(ParameterExpression source, ParameterExpression target) : ExpressionVisitor
        {
            private readonly ParameterExpression _source = source ?? throw new ArgumentNullException(nameof(source));
            private readonly ParameterExpression _target = target ?? throw new ArgumentNullException(nameof(target));

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _source ? _target : base.VisitParameter(node);
            }
        }
        private static Expression<Func<T, bool>> BuildStringFilter(ParameterExpression parameter, MemberExpression property, JsonElement element, FilterFn filterFn)
        {
            var propertyExpression = Expression.Lambda<Func<T, string>>(property, parameter);
            string value = element.GetString() ?? throw FilterExceptions.InvalidStringFilterException;
            return filterFn switch
            {
                FilterFn.Fuzzy => ExpressionExtensions.Contains(propertyExpression, value),
                FilterFn.Contains => ExpressionExtensions.Contains(propertyExpression, value),
                FilterFn.StartsWith => ExpressionExtensions.StartsWith(propertyExpression, value),
                FilterFn.EndsWith => ExpressionExtensions.EndsWith(propertyExpression, value),
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, value),
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, value),
                FilterFn.Empty => ExpressionExtensions.IsNullOrEmpty(propertyExpression),
                FilterFn.NotEmpty => ExpressionExtensions.IsNotNullOrEmpty(propertyExpression),
                _ => throw new NotImplementedException()
            };
        }
        private static Expression<Func<T, bool>> BuildInt32Filter(ParameterExpression parameter, MemberExpression property, JsonElement element, FilterFn filterFn)
        {
            var propertyExpression = Expression.Lambda<Func<T, int?>>(property, parameter);
            return filterFn switch
            {
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetInt32()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetInt32()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<int[]>().First(), element.Deserialize<int[]>().Last()) ?? throw FilterExceptions.InvalidNumericArrayFilterException,
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<int[]>().First(), element.Deserialize<int[]>().Last()) ?? throw FilterExceptions.InvalidNumericArrayFilterException,
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetInt32()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetInt32()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetInt32()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetInt32()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.Empty => ExpressionExtensions.IsNull(propertyExpression),
                FilterFn.NotEmpty => ExpressionExtensions.IsNotNull(propertyExpression),
                _ => throw new NotImplementedException(),
            };
        }
        private static Expression<Func<T, bool>> BuildInt64Filter(ParameterExpression parameter, MemberExpression property, JsonElement element, FilterFn filterFn)
        {
            var propertyExpression = Expression.Lambda<Func<T, long?>>(property, parameter);
            return filterFn switch
            {
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetInt64()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetInt64()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<long[]>().First(), element.Deserialize<long[]>().Last()) ?? throw FilterExceptions.InvalidNumericArrayFilterException,
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<long[]>().First(), element.Deserialize<long[]>().Last()) ?? throw FilterExceptions.InvalidNumericArrayFilterException,
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetInt64()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetInt64()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetInt64()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetInt64()) ?? throw FilterExceptions.InvalidNumericFilterException,
                FilterFn.Empty => ExpressionExtensions.IsNull(propertyExpression),
                FilterFn.NotEmpty => ExpressionExtensions.IsNotNull(propertyExpression),
                _ => throw new NotImplementedException(),
            };
        }
        private static Expression<Func<T, bool>> BuildDateTimeFilter(ParameterExpression parameter, MemberExpression property, JsonElement element, FilterFn filterFn)
        {
            var propertyExpression = Expression.Lambda<Func<T, DateTime?>>(property, parameter);
            return filterFn switch
            {
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetDateTime()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetDateTime()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<DateTime[]>().First(), element.Deserialize<DateTime[]>().Last()) ?? throw FilterExceptions.InvalidDateArrayFilterException,
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<DateTime[]>().First(), element.Deserialize<DateTime[]>().Last()) ?? throw FilterExceptions.InvalidDateArrayFilterException,
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetDateTime()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetDateTime()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetDateTime()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetDateTime()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.Empty => ExpressionExtensions.IsNull(propertyExpression),
                FilterFn.NotEmpty => ExpressionExtensions.IsNotNull(propertyExpression),
                _ => throw new NotImplementedException(),
            };
        }
        private static Expression<Func<T, bool>> BuildDateTimeOffsetFilter(ParameterExpression parameter, MemberExpression property, JsonElement element, FilterFn filterFn)
        {
            var propertyExpression = Expression.Lambda<Func<T, DateTimeOffset?>>(property, parameter);
            return filterFn switch
            {
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetDateTimeOffset()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetDateTimeOffset()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<DateTimeOffset[]>().First(), element.Deserialize<DateTimeOffset[]>().Last()) ?? throw FilterExceptions.InvalidDateArrayFilterException,
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<DateTimeOffset[]>().First(), element.Deserialize<DateTimeOffset[]>().Last()) ?? throw FilterExceptions.InvalidDateArrayFilterException,
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetDateTimeOffset()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetDateTimeOffset()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetDateTimeOffset()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetDateTimeOffset()) ?? throw FilterExceptions.InvalidDateFilterException,
                FilterFn.Empty => ExpressionExtensions.IsNull(propertyExpression),
                FilterFn.NotEmpty => ExpressionExtensions.IsNotNull(propertyExpression),
                _ => throw new NotImplementedException(),
            };
        }
        public Expression<Func<T, bool>> Build(List<FilterParams> filterParams)
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            var filters = new List<Expression<Func<T, bool>>>();
            foreach (var filter in filterParams)
            {
                var parameter = Expression.Parameter(typeof(T), Guid.NewGuid().ToString()[..5]);
                var property = Expression.Property(parameter, filter.Field);
                //var value = Expression.Constant(filter.Value);
                string propertyTypeName = property.Type.Name;
                bool isNullable = propertyTypeName.Contains("Nullable");
                if (isNullable)
                {
                    propertyTypeName = Nullable.GetUnderlyingType(property.Type).Name;
                }

                Expression<Func<T, bool>> expression = propertyTypeName switch
                {
                    "String" => PredicateBuilder<T>.BuildStringFilter(parameter, property, filter.Value, filter.FilterFn),
                    "Int32" => PredicateBuilder<T>.BuildInt32Filter(parameter, property, filter.Value, filter.FilterFn),
                    "Int64" => PredicateBuilder<T>.BuildInt64Filter(parameter, property, filter.Value, filter.FilterFn),
                    "DateTime" => PredicateBuilder<T>.BuildDateTimeFilter(parameter, property, filter.Value, filter.FilterFn),
                    "DateTimeOffset" => PredicateBuilder<T>.BuildDateTimeOffsetFilter(parameter, property, filter.Value, filter.FilterFn),
                    _ => throw new ArgumentOutOfRangeException(),
                };
                filters.Add(expression);
            }
            if (filters.Count == 0)
            {
                return _ => true;
            }

            return PredicateBuilder<T>.CombineWithAnd(filters);
        }
    }
}