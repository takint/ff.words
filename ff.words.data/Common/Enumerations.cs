namespace ff.words.data.Common
{
    public class Enumerations
    {
        public enum FilterStatementConnector
        {
            And,
            Or
        }

        public enum Operation
        {
            Equals,
            Contains,
            StartsWith,
            EndsWith,
            NotEquals,
            GreaterThan,
            GreaterThanOrEquals,
            LessThan,
            LessThanOrEquals,
            DoesNotContains,
            IsNull,
            IsNotNull,
            IsEmpty,
            IsNotEmpty
        }
    }
}