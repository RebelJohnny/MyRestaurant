using MyRestaurant.Framework.Querying.Filters;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;

namespace MyRestaurant.Framework.Querying.Filters
{
    public class PredicateBuilder<T> : IPredicateBuilder<T> where T : class
    {
        private List<Expression<Func<T, bool>>> Predicates = [];
        private Expression<Func<T, bool>> CombineWithAnd(IEnumerable<Expression<Func<T, bool>>> predicates)
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
            string value = element.GetString() ?? throw new InvalidStringFilterException();
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
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetInt32()) ?? throw new InvalidNumericFilterException(),
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetInt32()) ?? throw new InvalidNumericFilterException(),
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<int[]>().First(), element.Deserialize<int[]>().Last()) ?? throw new InvalidNumericArrayFilterException(),
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<int[]>().First(), element.Deserialize<int[]>().Last()) ?? throw new InvalidNumericArrayFilterException(),
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetInt32()) ?? throw new InvalidNumericFilterException(),
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetInt32()) ?? throw new InvalidNumericFilterException(),
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetInt32()) ?? throw new InvalidNumericFilterException(),
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetInt32()) ?? throw new InvalidNumericFilterException(),
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
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetInt64()) ?? throw new InvalidNumericFilterException(),
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetInt64()) ?? throw new InvalidNumericFilterException(),
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<long[]>().First(), element.Deserialize<long[]>().Last()) ?? throw new InvalidNumericArrayFilterException(),
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<long[]>().First(), element.Deserialize<long[]>().Last()) ?? throw new InvalidNumericArrayFilterException(),
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetInt64()) ?? throw new InvalidNumericFilterException(),
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetInt64()) ?? throw new InvalidNumericFilterException(),
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetInt64()) ?? throw new InvalidNumericFilterException(),
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetInt64()) ?? throw new InvalidNumericFilterException(),
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
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetDateTime()) ?? throw new InvalidDateFilterException(),
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetDateTime()) ?? throw new InvalidDateFilterException(),
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<DateTime[]>().First(), element.Deserialize<DateTime[]>().Last()) ?? throw new InvalidDateArrayFilterException(),
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<DateTime[]>().First(), element.Deserialize<DateTime[]>().Last()) ?? throw new InvalidDateArrayFilterException(),
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetDateTime()) ?? throw new InvalidDateFilterException(),
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetDateTime()) ?? throw new InvalidDateFilterException(),
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetDateTime()) ?? throw new InvalidDateFilterException(),
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetDateTime()) ?? throw new InvalidDateFilterException(),
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
                FilterFn.Equals => ExpressionExtensions.Equal(propertyExpression, element.GetDateTimeOffset()) ?? throw new InvalidDateFilterException(),
                FilterFn.NotEquals => ExpressionExtensions.NotEqual(propertyExpression, element.GetDateTimeOffset()) ?? throw new InvalidDateFilterException(),
                FilterFn.Between => ExpressionExtensions.Between(propertyExpression, element.Deserialize<DateTimeOffset[]>().First(), element.Deserialize<DateTimeOffset[]>().Last()) ?? throw new InvalidDateArrayFilterException(),
                FilterFn.BetweenInclusive => ExpressionExtensions.BetweenInclusive(propertyExpression, element.Deserialize<DateTimeOffset[]>().First(), element.Deserialize<DateTimeOffset[]>().Last()) ?? throw new InvalidDateArrayFilterException(),
                FilterFn.GreaterThan => ExpressionExtensions.GreaterThan(propertyExpression, element.GetDateTimeOffset()) ?? throw new InvalidDateFilterException(),
                FilterFn.GreaterThanOrEqualTo => ExpressionExtensions.GreaterThanOrEqual(propertyExpression, element.GetDateTimeOffset()) ?? throw new InvalidDateFilterException(),
                FilterFn.LessThan => ExpressionExtensions.LessThan(propertyExpression, element.GetDateTimeOffset()) ?? throw new InvalidDateFilterException(),
                FilterFn.LessThanOrEqualTo => ExpressionExtensions.LessThanOrEqual(propertyExpression, element.GetDateTimeOffset()) ?? throw new InvalidDateFilterException(),
                FilterFn.Empty => ExpressionExtensions.IsNull(propertyExpression),
                FilterFn.NotEmpty => ExpressionExtensions.IsNotNull(propertyExpression),
                _ => throw new NotImplementedException(),
            };
        }
        public Expression<Func<T, bool>> Build(List<FilterParams> filterParams)
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            var filterBuilder = new List<Expression<Func<T, bool>>>();
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
                Expression<Func<T, bool>> expression;
                switch (propertyTypeName)
                {
                    case "String":
                        expression = PredicateBuilder<T>.BuildStringFilter(parameter, property, filter.Value, filter.FilterFn);
                        break;

                    case "Int32":
                        expression = PredicateBuilder<T>.BuildInt32Filter(parameter, property, filter.Value, filter.FilterFn);
                        break;

                    case "Int64":
                        expression = PredicateBuilder<T>.BuildInt64Filter(parameter, property, filter.Value, filter.FilterFn);
                        break;

                    case "DateTime":
                        expression = PredicateBuilder<T>.BuildDateTimeFilter(parameter, property, filter.Value, filter.FilterFn);
                        break;

                    case "DateTimeOffset":
                        expression = PredicateBuilder<T>.BuildDateTimeOffsetFilter(parameter, property, filter.Value, filter.FilterFn);
                        break;
                    default:
                        break;
                }
            }
            foreach (var criteria in filterCriteria)
            {
                var parameter = Expression.Parameter(typeof(T), Guid.NewGuid().ToString()[..5]);
                var property = Expression.Property(parameter, criteria.Key);
                var value = Expression.Constant(criteria.Value.Item2);
                string propertyType = property.Type.Name;
                if (propertyType.Contains("Nullable"))
                {
                    propertyType = Nullable.GetUnderlyingType(property.Type).Name;
                }

                Expression<Func<T, bool>> expression;
                switch (propertyType)
                {
                    case "Int32":
                        switch (criteria.Value.Item1)
                        {
                            case "GreaterThan":
                                expression = ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThanOrEqual":
                                expression = ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item2));
                                break;
                            case "LessThan":
                                expression = ExpressionExtensions.LessThan(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item2));
                                break;
                            case "LessThanOrEqual":
                                expression = ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item2));
                                break;
                            case "Equals":
                                string[] values = criteria.Value.Item2.Split(',');
                                expression = property.Type.Name.Contains("Nullable") ?
                                 ExpressionExtensions.NumberArrayEqual32(Expression.Lambda<Func<T, int?>>(property, parameter), values)
                                : ExpressionExtensions.NumberArrayEqual32(Expression.Lambda<Func<T, int>>(property, parameter), values);
                                break;
                            case "NotEquals":
                                expression = property.Type.Name.Contains("Nullable") ?
                                    ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item2)) :
                                    ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item2));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (criteria.Value.Item4 != null && criteria.Value.Item4 != "")
                        {
                            switch (criteria.Value.Item5)
                            {
                                case LogicalOperator.And:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "GreaterThan":
                                            expression = expression.And(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.And(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "Equals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                            expression.And(ExpressionExtensions.Equal(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4))) :
                                            expression.And(ExpressionExtensions.Equal(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                                expression.And(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4))) :
                                                expression.And(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                case LogicalOperator.Or:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "GreaterThan":
                                            expression = expression.Or(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.Or(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "Equals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                            expression.Or(ExpressionExtensions.Equal(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4))) :
                                            expression.Or(ExpressionExtensions.Equal(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                                expression.Or(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, int?>>(property, parameter), Int32.Parse(criteria.Value.Item4))) :
                                                expression.Or(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, int>>(property, parameter), Int32.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        break;
                    case "Int64":
                        switch (criteria.Value.Item1)
                        {
                            case "GreaterThan":
                                expression = ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThanOrEqual":
                                expression = ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item2));
                                break;
                            case "LessThan":
                                expression = ExpressionExtensions.LessThan(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item2));
                                break;
                            case "LessThanOrEqual":
                                expression = ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item2));
                                break;
                            case "Equals":
                                string[] values = criteria.Value.Item2.Split(',');
                                expression = property.Type.Name.Contains("Nullable") ?
                                 ExpressionExtensions.NumberArrayEqual(Expression.Lambda<Func<T, long>>(property, parameter), values)
                                : ExpressionExtensions.NumberArrayEqual(Expression.Lambda<Func<T, long>>(property, parameter), values);
                                break;
                            case "NotEquals":
                                expression = property.Type.Name.Contains("Nullable") ?
                                    ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item2)) :
                                    ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item2));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (criteria.Value.Item4 != null && criteria.Value.Item4 != "")
                        {
                            switch (criteria.Value.Item5)
                            {
                                case LogicalOperator.And:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "GreaterThan":
                                            expression = expression.And(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.And(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "Equals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                            expression.And(ExpressionExtensions.Equal(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4))) :
                                            expression.And(ExpressionExtensions.Equal(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                                expression.And(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4))) :
                                                expression.And(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                case LogicalOperator.Or:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "GreaterThan":
                                            expression = expression.Or(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.Or(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "Equals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                            expression.Or(ExpressionExtensions.Equal(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4))) :
                                            expression.Or(ExpressionExtensions.Equal(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = property.Type.Name.Contains("Nullable") ?
                                                expression.Or(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, long?>>(property, parameter), Int64.Parse(criteria.Value.Item4))) :
                                                expression.Or(ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, long>>(property, parameter), Int64.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        break;
                    case "Double":
                        switch (criteria.Value.Item1)
                        {
                            case "GreaterThan":
                                expression = ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThanOrEqual":
                                expression = ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item2));
                                break;
                            case "LessThan":
                                expression = ExpressionExtensions.LessThan(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item2));
                                break;
                            case "LessThanOrEqual":
                                expression = ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item2));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (criteria.Value.Item4 != null && criteria.Value.Item4 != "")
                        {
                            switch (criteria.Value.Item5)
                            {
                                case LogicalOperator.And:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "GreaterThan":
                                            expression = expression.And(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.And(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                case LogicalOperator.Or:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "GreaterThan":
                                            expression = expression.Or(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.Or(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, double>>(property, parameter), double.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        break;
                    case "Decimal":
                        switch (criteria.Value.Item1)
                        {
                            case "Equals":
                                expression = ExpressionExtensions.Equal(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                break;
                            case "NotEquals":
                                expression = ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThan":
                                expression = ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThanOrEqual":
                                expression = ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                break;
                            case "LessThan":
                                expression = ExpressionExtensions.LessThan(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                break;
                            case "LessThanOrEqual":
                                expression = ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (criteria.Value.Item4 != null && criteria.Value.Item4 != "")
                        {
                            switch (criteria.Value.Item5)
                            {
                                case LogicalOperator.And:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "Equals":
                                            expression = ExpressionExtensions.Equal(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                            break;
                                        case "NotEquals":
                                            expression = ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                            break;
                                        case "GreaterThan":
                                            expression = expression.And(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.And(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                case LogicalOperator.Or:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "Equals":
                                            expression = ExpressionExtensions.Equal(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                            break;
                                        case "NotEquals":
                                            expression = ExpressionExtensions.NotEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item2));
                                            break;
                                        case "GreaterThan":
                                            expression = expression.Or(ExpressionExtensions.GreaterThan(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.GreaterThanOrEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.Or(ExpressionExtensions.LessThan(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.LessThanOrEqual(Expression.Lambda<Func<T, decimal>>(property, parameter), decimal.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        break;
                    case "DateTimeOffset":
                        switch (criteria.Value.Item1)
                        {
                            case "Equals":
                                expression = ExpressionExtensions.DateTimeEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item2));
                                break;
                            case "NotEquals":
                                expression = ExpressionExtensions.DateTimeNotEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThan":
                                expression = ExpressionExtensions.DateTimeGreaterThanForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThanOrEqual":
                                expression = ExpressionExtensions.DateTimeGreaterThanOrEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item2));
                                break;
                            case "LessThan":
                                expression = ExpressionExtensions.DateTimeLessThanDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item2));
                                break;
                            case "LessThanOrEqual":
                                expression = ExpressionExtensions.DateTimeLessThanOrEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item2));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (criteria.Value.Item4 != null && criteria.Value.Item4 != "")
                        {
                            switch (criteria.Value.Item5)
                            {
                                case LogicalOperator.And:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "Equals":
                                            expression = expression.And(ExpressionExtensions.DateTimeEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = expression.And(ExpressionExtensions.DateTimeNotEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThan":
                                            expression = expression.And(ExpressionExtensions.DateTimeLessThanForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.DateTimeGreaterThanOrEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.And(ExpressionExtensions.DateTimeLessThanDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.DateTimeLessThanOrEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                case LogicalOperator.Or:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "Equals":
                                            expression = expression.Or(ExpressionExtensions.DateTimeEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = expression.Or(ExpressionExtensions.DateTimeNotEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThan":
                                            expression = expression.Or(ExpressionExtensions.DateTimeGreaterThanForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.DateTimeGreaterThanOrEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.Or(ExpressionExtensions.DateTimeLessThanDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.DateTimeLessThanOrEqualForDateTimeOfSet(Expression.Lambda<Func<T, DateTimeOffset>>(property, parameter), DateTimeOffset.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        break;
                    case "DateTime":
                        switch (criteria.Value.Item1)
                        {
                            case "Equals":
                                expression = ExpressionExtensions.DateTimeEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            case "NotEquals":
                                expression = ExpressionExtensions.DateTimeNotEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThan":
                                expression = ExpressionExtensions.DateTimeGreaterThan(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            case "GreaterThanOrEqual":
                                expression = ExpressionExtensions.DateTimeGreaterThanOrEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            case "LessThan":
                                expression = ExpressionExtensions.DateTimeLessThan(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            case "LessThanOrEqual":
                                expression = ExpressionExtensions.DateTimeLessThanOrEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item2));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        if (criteria.Value.Item4 != null && criteria.Value.Item4 != "")
                        {
                            switch (criteria.Value.Item5)
                            {
                                case LogicalOperator.And:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "Equals":
                                            expression = expression.And(ExpressionExtensions.DateTimeEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = expression.And(ExpressionExtensions.DateTimeNotEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThan":
                                            expression = expression.And(ExpressionExtensions.DateTimeGreaterThan(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.DateTimeGreaterThanOrEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.And(ExpressionExtensions.DateTimeLessThan(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.And(ExpressionExtensions.DateTimeLessThanOrEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                case LogicalOperator.Or:
                                    switch (criteria.Value.Item3)
                                    {
                                        case "Equals":
                                            expression = expression.Or(ExpressionExtensions.DateTimeEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "NotEquals":
                                            expression = expression.Or(ExpressionExtensions.DateTimeNotEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThan":
                                            expression = expression.Or(ExpressionExtensions.DateTimeGreaterThan(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "GreaterThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.DateTimeGreaterThanOrEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThan":
                                            expression = expression.Or(ExpressionExtensions.DateTimeLessThan(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        case "LessThanOrEqual":
                                            expression = expression.Or(ExpressionExtensions.DateTimeLessThanOrEqual(Expression.Lambda<Func<T, DateTime>>(property, parameter), DateTime.Parse(criteria.Value.Item4)));
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                filterBuilder.Add(expression);
                logicalOperators.Add(criteria.Value.Item5);
            }
            if (filterBuilder.Count == 0)
            {
                return null;
            }


            return CombineWithAnd(filterBuilder);
        }
    }
}