namespace ff.words.data.Common.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using static ff.words.data.Common.Enumerations;

    public interface IFilter<TClass> where TClass : class
    {
        /// <summary>
        /// Group of statements that compose this filter.
        /// </summary>
        IEnumerable<IFilterStatement> Statements { get; }

        /// <summary>
        /// Adds another statement to this filter.
        /// </summary>
        /// <typeparam name="TPropertyType">Generic type.</typeparam>
        /// <param name="propertyName">Name of the property that will be filtered.</param>
        /// <param name="operation">Express the interaction between the property and the constant value.</param>
        /// <param name="value">Constant value that will interact with the property.</param>
        /// <param name="connector">Establishes how this filter statement will connect to the next one.</param>
        /// <returns>A FilterStatementConnection object that defines how this statement will be connected to the next one.</returns>
        IFilterStatementConnection<TClass> By<TPropertyType>(string propertyName, Operation operation, TPropertyType value, FilterStatementConnector connector = FilterStatementConnector.And, bool? isReplaceAllSpace = false);

        /// <summary>
        /// Removes all statements from this filter.
        /// </summary>
        void Clear();

        /// <summary>
        /// Builds a LINQ expression based upon the statements included in this filter.
        /// </summary>
        /// <returns>Linq Statement</returns>
        Expression<Func<TClass, bool>> BuildExpression();
    }
}
