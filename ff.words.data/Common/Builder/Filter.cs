namespace ff.words.data.Common.Builder
{
    using System;
    using System.Collections.Generic;
    using static ff.words.data.Common.Enumerations;

    public class Filter<TClass> : IFilter<TClass> where TClass : class
    {
        private readonly List<IFilterStatement> _statements;

        private bool? _isReplaceAllSpace;

        public Filter()
        {
            _statements = new List<IFilterStatement>();
        }

        public IEnumerable<IFilterStatement> Statements
        {
            get
            {
                return _statements.ToArray();
            }
        }

        public IFilterStatementConnection<TClass> By<TPropertyType>(string propertyName, Operation operation, TPropertyType value, FilterStatementConnector connector = FilterStatementConnector.And, bool? isReplaceAllSpace = false)
        {
            _isReplaceAllSpace = isReplaceAllSpace;
            IFilterStatement statement = null;
            statement = new FilterStatement<TPropertyType>(propertyName, operation, value, connector);
            _statements.Add(statement);
            return new FilterStatementConnection<TClass>(this, statement);
        }

        public void Clear()
        {
            _statements.Clear();
        }

        public System.Linq.Expressions.Expression<Func<TClass, bool>> BuildExpression()
        {
            return DynammicBuilder.GetExpression<TClass>(this, _isReplaceAllSpace);
        }

        public override string ToString()
        {
            var result = string.Empty;
            FilterStatementConnector lastConector = FilterStatementConnector.And;
            foreach (var statement in _statements)
            {
                if (!string.IsNullOrWhiteSpace(result))
                {
                    result += " " + lastConector + " ";
                }

                result += statement.ToString();
                lastConector = statement.Connector;
            }

            return result.Trim();
        }
    }
}
