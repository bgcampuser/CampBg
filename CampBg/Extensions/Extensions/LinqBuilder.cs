namespace CampBg.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class LinqBuilder
    {
        public static Expression<Func<TElement, bool>> BuildOrExpression<TElement, TValue>(
                Expression<Func<TElement, TValue>> valueSelector,
                IEnumerable<TValue> values)
        {
            if (null == valueSelector)
            {
                throw new ArgumentNullException("valueSelector");
            }

            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            var p = valueSelector.Parameters.Single();

            if (!values.Any())
            {
                return e => false;
            }

            var equals = values.Select(value =>
                (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate(Expression.Or);

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
    }
}