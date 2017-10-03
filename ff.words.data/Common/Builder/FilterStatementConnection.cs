namespace ff.words.data.Common.Builder
{
    using static ff.words.data.Common.Enumerations;

    public class FilterStatementConnection<TClass> : IFilterStatementConnection<TClass> where TClass : class
    {
        private readonly IFilter<TClass> _filter;
        private readonly IFilterStatement _statement;

        public FilterStatementConnection(IFilter<TClass> filter, IFilterStatement statement)
        {
            _filter = filter;
            _statement = statement;
        }

        public IFilter<TClass> And
        {
            get
            {
                _statement.Connector = FilterStatementConnector.And;
                return _filter;
            }
        }

        public IFilter<TClass> Or
        {
            get
            {
                _statement.Connector = FilterStatementConnector.Or;
                return _filter;
            }
        }
    }
}
