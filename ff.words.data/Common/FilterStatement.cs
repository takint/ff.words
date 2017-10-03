﻿namespace ff.words.data.Common
{
    using System;
    using System.Collections.Generic;
    using static ff.words.data.Common.Enumerations;

    public class FilterStatement<TPropertyType> : IFilterStatement
    {
        public FilterStatement(string propertyName, Operation operation, TPropertyType value, FilterStatementConnector connector = FilterStatementConnector.And)
        {
            PropertyName = propertyName;
            Connector = connector;
            Operation = operation;
            if (typeof(TPropertyType).IsArray)
            {
                if (operation != Operation.Contains)
                {
                    throw new ArgumentException("Only 'Operacao.Contains' supports arrays as parameters.");
                }

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(typeof(TPropertyType).GetElementType());
                Value = Activator.CreateInstance(constructedListType, value);
            }
            else
            {
                Value = value;
            }
        }

        public FilterStatementConnector Connector { get; set; }

        public string PropertyName { get; set; }

        public Operation Operation { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", PropertyName, Operation, Value);
        }
    }
}
