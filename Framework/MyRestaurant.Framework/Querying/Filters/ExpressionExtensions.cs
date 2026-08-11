using MyRestaurant.Framework.Querying.Filters;
using System.Linq.Expressions;

namespace MyRestaurant.Framework.Querying.Filters
{
    public static class ExpressionExtensions
    {
        #region Shared
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, invokedExpr), left.Parameters);
        }
        public static Expression<Func<T, bool>> NotEqual<T, TValue>(this Expression<Func<T, TValue>> property, TValue value)
        {
            var expression = Expression.NotEqual(property.Body, Expression.Constant(value, typeof(TValue)));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> Equal<T, TValue>(this Expression<Func<T, TValue>> property, TValue value)
        {
            var expression = Expression.Equal(property.Body, Expression.Constant(value, typeof(TValue)));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> IsNull<T, TProperty>(this Expression<Func<T, TProperty>> property)
        {
            if (typeof(TProperty).IsValueType && Nullable.GetUnderlyingType(typeof(TProperty)) is null)
            {
                return _ => false;
            }
            var expression = Expression.Equal(property.Body, Expression.Constant(null, typeof(TProperty)));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> IsNotNull<T, TProperty>(this Expression<Func<T, TProperty>> property)
        {
            if (typeof(TProperty).IsValueType && Nullable.GetUnderlyingType(typeof(TProperty)) is null)
            {
                return _ => false;
            }
            var expression = Expression.Equal(property.Body, Expression.Constant(null, typeof(TProperty)));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        #endregion
        #region String
        public static Expression<Func<T, bool>> Contains<T>(this Expression<Func<T, string>> property, string value)
        {
            var method = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)])!;
            var expression = Expression.Call(property.Body, method, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DoesNotContain<T>(this Expression<Func<T, string>> property, string value)
        {
            var method = typeof(string).GetMethod("Contains", [typeof(string)])!;
            var expression = Expression.Not(Expression.Call(property.Body, method, Expression.Constant(value)));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> StartsWith<T>(this Expression<Func<T, string>> property, string value)
        {
            var method = typeof(string).GetMethod("StartsWith", [typeof(string)])!;
            var expression = Expression.Call(property.Body, method, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> EndsWith<T>(this Expression<Func<T, string>> property, string value)
        {
            var method = typeof(string).GetMethod("EndsWith", [typeof(string)])!;
            var expression = Expression.Call(property.Body, method, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> IsNullOrEmpty<T>(this Expression<Func<T, string>> property)
        {
            var method = typeof(string).GetMethod(nameof(string.IsNullOrEmpty), [typeof(string)])!;
            var expression = Expression.Call(method, property.Body);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> IsNotNullOrEmpty<T>(this Expression<Func<T, string>> property)
        {
            var method = typeof(string).GetMethod(nameof(string.IsNullOrEmpty), [typeof(string)])!;
            var expression = Expression.Not(Expression.Call(method, property.Body));
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        #endregion
        #region Numeric
        public static Expression<Func<T, bool>> GreaterThan<T, TValue>(this Expression<Func<T, TValue>> property, TValue value)
        {
            var constant = Expression.Constant(value, typeof(TValue));
            var expression = Expression.GreaterThan(property.Body, constant);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> GreaterThanOrEqual<T, TValue>(this Expression<Func<T, TValue>> property, TValue value)
        {
            var constant = Expression.Constant(value, typeof(TValue));
            var expression = Expression.GreaterThanOrEqual(property.Body, constant);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> LessThan<T, TValue>(this Expression<Func<T, TValue>> property, TValue value)
        {
            var constant = Expression.Constant(value, typeof(TValue));
            var expression = Expression.LessThan(property.Body, constant);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> LessThanOrEqual<T, TValue>(this Expression<Func<T, TValue>> property, TValue value)
        {
            var constant = Expression.Constant(value, typeof(TValue));
            var expression = Expression.LessThanOrEqual(property.Body, constant);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> Between<T, TValue>(this Expression<Func<T, TValue>> property, TValue startValue, TValue endValue)
        {
            var startConstant = Expression.Constant(startValue, typeof(TValue));
            var startExpression = Expression.GreaterThan(property.Body, startConstant);

            var endConstant = Expression.Constant(endValue, typeof(TValue));
            var endExpression = Expression.LessThan(property.Body, endConstant);

            var expression = Expression.AndAlso(startExpression, endExpression);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> BetweenInclusive<T, TValue>(this Expression<Func<T, TValue>> property, TValue startValue, TValue endValue)
        {
            var startConstant = Expression.Constant(startValue, typeof(TValue));
            var startExpression = Expression.GreaterThan(property.Body, startConstant);

            var endConstant = Expression.Constant(endValue, typeof(TValue));
            var endExpression = Expression.LessThan(property.Body, endConstant);

            var expression = Expression.AndAlso(startExpression, endExpression);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        #endregion
        #region Boolean
        public static Expression<Func<T, bool>> IsTrue<T>(this Expression<Func<T, bool>> property)
        {
            ConstantExpression trueConstant = Expression.Constant(true);
            var expression = Expression.Equal(property.Body, trueConstant);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> IsFalse<T>(this Expression<Func<T, bool>> property)
        {
            ConstantExpression trueConstant = Expression.Constant(false);
            var expression = Expression.Equal(property.Body, trueConstant);
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        #endregion
        #region DateTime
        public static Expression<Func<T, bool>> DateTimeEqual<T>(this Expression<Func<T, DateTime>> property, DateTime value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTime));
            var expression = Expression.Equal(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTime));
                expression = Expression.Equal(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeNotEqual<T>(this Expression<Func<T, DateTime>> property, DateTime value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTime));
            var expression = Expression.NotEqual(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTime));
                expression = Expression.NotEqual(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeGreaterThan<T>(this Expression<Func<T, DateTime>> property, DateTime value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTime));
            var expression = Expression.GreaterThan(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTime));
                expression = Expression.GreaterThan(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeLessThan<T>(this Expression<Func<T, DateTime>> property, DateTime value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTime));
            var expression = Expression.LessThan(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTime));
                expression = Expression.LessThan(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeGreaterThanOrEqual<T>(this Expression<Func<T, DateTime>> property, DateTime value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTime));
            var expression = Expression.GreaterThanOrEqual(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTime));
                expression = Expression.GreaterThanOrEqual(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeLessThanOrEqual<T>(this Expression<Func<T, DateTime>> property, DateTime value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTime));
            var expression = Expression.LessThanOrEqual(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTime));
                expression = Expression.LessThanOrEqual(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        #endregion
        #region DateTimeOffset
        public static Expression<Func<T, bool>> DateTimeEqualForDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.Equal(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.Equal(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeNotEqualForDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.NotEqual(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.NotEqual(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeGreaterThanForDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.GreaterThan(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.GreaterThan(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeLessThanForDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.LessThan(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.LessThan(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeLessThanDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.LessThan(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.LessThan(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeGreaterThanOrEqualForDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.GreaterThanOrEqual(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.GreaterThanOrEqual(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> DateTimeLessThanOrEqualForDateTimeOfSet<T>(this Expression<Func<T, DateTimeOffset>> property, DateTimeOffset value, DateTimeKind kind = DateTimeKind.Unspecified)
        {
            var constant = Expression.Constant(value, typeof(DateTimeOffset));
            var expression = Expression.LessThanOrEqual(property.Body, constant);
            if (kind != DateTimeKind.Unspecified)
            {
                var convertExpression = Expression.Convert(constant, typeof(DateTimeOffset));
                expression = Expression.LessThanOrEqual(property.Body, convertExpression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        #endregion

        public static Expression<Func<T, bool>> NumberArrayEqual<T, TValue>(this Expression<Func<T, TValue>> property, string[] values)
        {
            ConstantExpression constantExpression = Expression.Constant(Convert.ToInt64(values[0]), typeof(TValue));
            BinaryExpression expression = Expression.Equal(property.Body, constantExpression);
            for (int i = 1; i < values.Length; i++)
            {
                ConstantExpression iteratingConstant = Expression.Constant(Convert.ToInt64(values[i]), typeof(TValue));
                BinaryExpression equality = Expression.Equal(property.Body, iteratingConstant);
                expression = Expression.OrElse(expression, equality);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }
        public static Expression<Func<T, bool>> NumberArrayEqual32<T, TValue>(this Expression<Func<T, TValue>> property, string[] values)
        {
            ConstantExpression constantExpression = Expression.Constant(Convert.ToInt32(values[0]), typeof(TValue));
            BinaryExpression expression = Expression.Equal(property.Body, constantExpression);
            for (int i = 1; i < values.Length; i++)
            {
                ConstantExpression iteratingConstant = Expression.Constant(Convert.ToInt32(values[i]), typeof(TValue));
                BinaryExpression equality = Expression.Equal(property.Body, iteratingConstant);
                expression = Expression.OrElse(expression, equality);
            }
            return Expression.Lambda<Func<T, bool>>(expression, property.Parameters);
        }


        public static Expression<Func<T, bool>> CombineWithAnd<T>(IEnumerable<Expression<Func<T, bool>>> predicates)
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
    }
}