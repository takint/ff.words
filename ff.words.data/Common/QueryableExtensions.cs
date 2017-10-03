﻿namespace ff.words.data.Common
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Microsoft.EntityFrameworkCore.Storage;

    using Remotion.Linq.Parsing.Structure;

    public static class QueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly PropertyInfo NodeTypeProviderField = QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");

        private static readonly MethodInfo CreateQueryParserMethod = QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly FieldInfo QueryCompilationContextFactoryField = typeof(Database).GetTypeInfo().DeclaredFields.Single(x => x.Name == "_queryCompilationContextFactory");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            try
            {
                var queryCompiler = (IQueryCompiler)QueryCompilerField.GetValue(query.Provider);
                var nodeTypeProvider = (INodeTypeProvider)NodeTypeProviderField.GetValue(queryCompiler);
                var parser =
                    (IQueryParser)CreateQueryParserMethod.Invoke(queryCompiler, new object[] { nodeTypeProvider });
                var queryModel = parser.GetParsedQuery(query.Expression);
                var database = DataBaseField.GetValue(queryCompiler);
                var queryCompilationContextFactory =
                    (IQueryCompilationContextFactory)QueryCompilationContextFactoryField.GetValue(database);
                var queryCompilationContext = queryCompilationContextFactory.Create(false);
                var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();

                modelVisitor.CreateQueryExecutor<TEntity>(queryModel);

                var sql = modelVisitor.Queries.First().ToString();

                return sql;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static bool IsOrdered<TEntity>(this IQueryable<TEntity> queryable) where TEntity : class
        {
            string sqlString = queryable.ToSql();

            int index = sqlString.LastIndexOf(')');

            if (index == -1)
            {
                index = 0;
            }

            if (sqlString.IndexOf("ORDER BY", index) != -1)
            {
                return true;
            }

            return false;
        }
    }
}
