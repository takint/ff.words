namespace ff.words.data.Repository
{
    using ff.words.data.Context;
    using ff.words.data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected FFWordsContext dbContext;

        protected DbSet<TEntity> dbSet;

        public Repository(FFWordsContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual void Add(TEntity obj)
        {
            dbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            dbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
