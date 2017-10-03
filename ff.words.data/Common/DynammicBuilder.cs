namespace ff.words.data.Common
{
    using ff.words.data.Common.Builder;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using static ff.words.data.Common.Enumerations;

    public static class DynammicBuilder
    {
        private static readonly Dictionary<Operation, Func<Expression, Expression, Expression>> Expressions;

        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo isNullOrEmptyMethod = typeof(string).GetMethod("IsNullOrEmpty");
        private static MethodInfo trimMethod = typeof(string).GetMethod("Trim", new Type[0]);
        private static MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);
        private static MethodInfo replaceMethod = typeof(string).GetMethod("Replace", new[] { typeof(string), typeof(string) });
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        static DynammicBuilder()
        {
            Expressions = new Dictionary<Operation, Func<Expression, Expression, Expression>>();
            Expressions.Add(Operation.Equals, (member, constant) => Expression.Equal(member, constant));
            Expressions.Add(Operation.NotEquals, (member, constant) => Expression.NotEqual(member, constant));
            Expressions.Add(Operation.GreaterThan, (member, constant) => Expression.GreaterThan(member, constant));
            Expressions.Add(Operation.GreaterThanOrEquals, (member, constant) => Expression.GreaterThanOrEqual(member, constant));
            Expressions.Add(Operation.LessThan, (member, constant) => Expression.LessThan(member, constant));
            Expressions.Add(Operation.LessThanOrEquals, (member, constant) => Expression.LessThanOrEqual(member, constant));
            Expressions.Add(Operation.Contains, (member, constant) => Contains(member, constant));
            Expressions.Add(Operation.DoesNotContains, (member, constant) => Expression.Not(Contains(member, constant)));
            Expressions.Add(Operation.StartsWith, (member, constant) => Expression.Call(member, startsWithMethod, constant));
            Expressions.Add(Operation.EndsWith, (member, constant) => Expression.Call(member, endsWithMethod, constant));
            Expressions.Add(Operation.IsNull, (member, constant) => Expression.Equal(member, constant));
            Expressions.Add(Operation.IsNotNull, (member, constant) => Expression.NotEqual(member, constant));
            Expressions.Add(Operation.IsEmpty, (member, constant) => Expression.Equal(member, constant));
            Expressions.Add(Operation.IsNotEmpty, (member, constant) => Expression.NotEqual(member, constant));
        }

        public static Expression<Func<T, bool>> GetExpression<T>(IFilter<T> filter, bool? isReplaceAllSpace) where T : class
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression expression = Expression.Constant(true);
            var connector = FilterStatementConnector.And;

            foreach (var statement in filter.Statements)
            {
                Expression expr = null;
                if (IsList(statement))
                {
                    expr = ProcessListStatement(param, statement);
                }
                else
                {
                    expr = GetExpression(param, statement, null, isReplaceAllSpace);
                }

                expression = CombineExpressions(expression, expr, connector);
                connector = statement.Connector;
            }

            return Expression.Lambda<Func<T, bool>>(expression, param);
        }

        private static bool IsList(IFilterStatement statement)
        {
            return statement.PropertyName.Contains("[") && statement.PropertyName.Contains("]");
        }

        private static Expression CombineExpressions(Expression expr1, Expression expr2, FilterStatementConnector connector)
        {
            return connector == FilterStatementConnector.And ? Expression.AndAlso(expr1, expr2) : Expression.OrElse(expr1, expr2);
        }

        private static Expression ProcessListStatement(ParameterExpression param, IFilterStatement statement)
        {
            var basePropertyName = statement.PropertyName.Substring(0, statement.PropertyName.IndexOf("["));
            var propertyName = statement.PropertyName.Replace(basePropertyName, string.Empty).Replace("[", string.Empty).Replace("].", string.Empty);

            var type = param.Type.GetProperty(basePropertyName).PropertyType.GetGenericArguments()[0];
            ParameterExpression listItemParam = Expression.Parameter(type, "i");
            var lambda = Expression.Lambda(GetExpression(listItemParam, statement, propertyName), listItemParam);
            var member = GetMemberExpression(param, basePropertyName);
            var tipoEnumerable = typeof(Enumerable);
            var anyInfo = tipoEnumerable.GetMethods(BindingFlags.Static | BindingFlags.Public).First(m => m.Name == "Any" && m.GetParameters().Count() == 2);
            anyInfo = anyInfo.MakeGenericMethod(type);
            return Expression.Call(anyInfo, member, lambda);
        }

        private static Expression GetExpression(ParameterExpression param, IFilterStatement statement, string propertyName = null, bool? isReplaceAllSpace = false)
        {
            Expression member = GetMemberExpression(param, propertyName ?? statement.PropertyName);
            Expression constant;

            if (statement.Value == null)
            {
                if (statement.Operation == Operation.IsEmpty || statement.Operation == Operation.IsNotEmpty)
                {
                    return Expressions[statement.Operation].Invoke(member, Expression.Constant(string.Empty));
                }

                if (member.Type == typeof(DateTime))
                {
                    return Expressions[statement.Operation].Invoke(member, Expression.Constant(DateTime.MinValue));
                }

                return Expressions[statement.Operation].Invoke(member, Expression.Constant(statement.Value));
            }

            var currentStatementValueType = statement.Value.GetType();

            if (currentStatementValueType == typeof(string))
            {
                if (member.Type == typeof(string))
                {
                    constant = Expression.Constant(statement.Value, typeof(string));
                    var trimMemberCall = Expression.Call(member, trimMethod);

                    if (isReplaceAllSpace.Value)
                    {
                        var toLowerCall = Expression.Call(trimMemberCall, toLowerMethod);
                        var constant1 = Expression.Constant(" ", typeof(string));
                        var constant2 = Expression.Constant(string.Empty, typeof(string));
                        member = Expression.Call(toLowerCall, replaceMethod, constant1, constant2);
                    }
                    else
                    {
                        member = Expression.Call(trimMemberCall, toLowerMethod);
                    }

                    var trimConstantCall = Expression.Call(constant, trimMethod);
                    constant = Expression.Call(trimConstantCall, toLowerMethod);
                }
                else
                {
                    Type u = Nullable.GetUnderlyingType(member.Type);
                    if(u != null && u == typeof(int))
                    {
                        int? converted = statement.Value.ToString().ToNullableInt();
                        constant = Expression.Constant(converted);
                    }
                    else if (member.Type.GetTypeInfo().IsEnum)
                    {
                        constant = Expression.Constant(Enum.Parse(member.Type, statement.Value.ToString()));
                    }
                    else if (member.Type == typeof(DateTime))
                    {
                        var converted = DateTime.Parse(statement.Value.ToString()).ToUniversalTime();
                        constant = Expression.Constant(converted);
                    }
                    else
                    {
                        var parseMethod = member.Type.GetMethod("Parse", new[] { typeof(string) });
                        var converted = parseMethod.Invoke(null, new[] { statement.Value });
                        constant = Expression.Constant(converted);
                    }
                }
            }
            else
            {
                constant = Expression.Constant(statement.Value);
                var parseMethod = currentStatementValueType.GetMethod("Parse", new[] { typeof(string) });
                UnaryExpression converted = Expression.Convert(member, typeof(decimal), parseMethod);

                return Expressions[statement.Operation].Invoke(converted, constant); 
            }

            return Expressions[statement.Operation].Invoke(member, constant);
        }

        private static Expression Contains(Expression member, Expression expression)
        {
            if (expression is ConstantExpression)
            {
                var constant = (ConstantExpression)expression;
                var isGenericType = constant.Value.GetType().GetTypeInfo().IsGenericType;

                if (constant.Value is IList && isGenericType)
                {
                    var type = constant.Value.GetType();
                    var containsInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });
                    var contains = Expression.Call(constant, containsInfo, member);
                    return contains;
                }
            }

            return Expression.Call(member, containsMethod, expression);
        }

        private static MemberExpression GetMemberExpression(Expression param, string propertyName)
        {
            if (propertyName.Contains("."))
            {
                int index = propertyName.IndexOf(".");
                var subParam = Expression.Property(param, propertyName.Substring(0, index));
                return GetMemberExpression(subParam, propertyName.Substring(index + 1));
            }

            return Expression.Property(param, propertyName);
        }

        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
    }
}
