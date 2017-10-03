namespace ff.words.data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using static ff.words.data.Common.Enumerations;

    public class Filter<TEntity> where TEntity : class
    {
        public Filter(Expression<Func<TEntity, bool>> expression)
        {
            Expression = expression;
        }

        public Filter(FilterRequest filterRequest, Dictionary<string, string> filterMaps = null)
        {
            Expression = BuildExpression(filterRequest, filterMaps);
        }

        public Expression<Func<TEntity, bool>> Expression { get; private set; }

        protected virtual Dictionary<string, Enumerations.Operation> OperationDictionary { get; set; }

        public Expression<Func<TEntity, bool>> BuildExpression(FilterRequest filterRequest, Dictionary<string, string> filterMaps)
        {
            // If no sortby field is supplied, use the default.
            if (filterRequest == null)
            {
                return null;
            }

            var filter = new Builder.Filter<TEntity>();

            foreach (var filterDetail in filterRequest.Filters)
            {
                // For multi-values filter
                FilterStatementConnector connectLogic = filterRequest.Logic == "or" ? FilterStatementConnector.Or : FilterStatementConnector.And;
                if (filterDetail.Field == "Deleted")
                {
                    connectLogic = FilterStatementConnector.And;
                }

                filter.By(filterDetail.Field, this.OperationDictionary[filterDetail.Operator], filterDetail.Value, connectLogic);
            }

            return filter.BuildExpression();
        }

        public void AddExpression(Expression<Func<TEntity, bool>> newExpression)
        {
            if (newExpression == null)
            {
                throw new ArgumentNullException(nameof(newExpression), $"{nameof(newExpression)} is null.");
            }

            if (Expression == null)
            {
                Expression = newExpression;
            }

            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(TEntity));

            var leftVisitor = new ReplaceExpressionVisitor(newExpression.Parameters[0], parameter);
            var left = leftVisitor.Visit(newExpression.Body);

            var rightVisitor = new ReplaceExpressionVisitor(Expression.Parameters[0], parameter);
            var right = rightVisitor.Visit(Expression.Body);

            Expression = System.Linq.Expressions.Expression.Lambda<Func<TEntity, bool>>(System.Linq.Expressions.Expression.AndAlso(left, right), parameter);
        }
    }
}
