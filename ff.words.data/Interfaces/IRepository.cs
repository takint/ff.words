namespace ff.words.data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        void Add(TEntity obj);

        void Update(TEntity obj);

        void Remove(int id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
