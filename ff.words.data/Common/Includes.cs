namespace ff.words.data.Common
{
    using System;
    using System.Linq;

    public class Includes<TEntity>
    {
        public Includes(Func<IQueryable<TEntity>, IQueryable<TEntity>> expression)
        {
            Expression = expression;
        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Expression { get; private set; }
    }
}
