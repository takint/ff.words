namespace ff.words.data.Repository
{
    using ff.words.data.Common;
    using ff.words.data.Context;
    using ff.words.data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected FFWordsContext dbContext;

        protected DbSet<TEntity> dbSet;

        /// <summary>
        /// Only override this for Database View
        /// </summary>
        protected virtual string ViewSql { get; }

        public virtual OrderBy<TEntity> DefaultOrderBy => new OrderBy<TEntity>(qry => qry.OrderByDescending(e => e.UpdatedDate).ThenByDescending(e => e.CreatedDate));

        public virtual Dictionary<SortRequest, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> OrderByColumnMaps { get; set; }

        public virtual Dictionary<string, string> FilterMaps => new Dictionary<string, string>();

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludePropertiesForDetail { get; }

        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludePropertiesForList { get; }

        public Repository(FFWordsContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            return query;
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
            if (obj == null)
            {
                throw new InvalidOperationException("Unable to add a null entity to the repository.");
            }

            dbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            dbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
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

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return GetAll(orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(null, orderBy, includes);
            return result.ToList();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return GetAllAsync(orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(null, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            Load(orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual void Load(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(null, orderBy, includes);
            result.Load();
        }

        public virtual Task LoadAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return LoadAsync(orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task LoadAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(null, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> GetPage(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return GetPage(startRow, pageLength, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual IEnumerable<TEntity> GetPage(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            if (orderBy == null)
            {
                orderBy = this.DefaultOrderBy.Expression;
            }

            var result = QueryDb(null, orderBy, includes);

            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual Task<IEnumerable<TEntity>> GetPageAsync(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return GetPageAsync(startRow, pageLength, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task<IEnumerable<TEntity>> GetPageAsync(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            if (orderBy == null)
            {
                orderBy = this.DefaultOrderBy.Expression;
            }

            var result = QueryDb(null, orderBy, includes);

            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual TEntity Get(int id, bool includeProperties = false)
        {
            return Get(id, includeProperties ? this.IncludePropertiesForDetail : null);
        }

        public virtual TEntity Get(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public virtual Task<TEntity> GetAsync(int id, bool includeProperties = false)
        {
            return GetAsync(id, includeProperties ? this.IncludePropertiesForDetail : null);
        }

        public virtual Task<TEntity> GetAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return Query(filter, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(filter, orderBy, includes);
            return result.ToList();
        }

        public virtual Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return QueryAsync(filter, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(filter, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            Load(filter, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual void Load(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(filter, orderBy, includes);
            result.Load();
        }

        public virtual Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return LoadAsync(filter, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task LoadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var result = QueryDb(filter, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> QueryPage(int? startRow, int? pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return QueryPage(startRow, pageLength, filter, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual IEnumerable<TEntity> QueryPage(int? startRow, int? pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            if (orderBy == null)
            {
                orderBy = this.DefaultOrderBy.Expression;
            }

            var result = QueryDb(filter, orderBy, includes);
            if (startRow.HasValue && pageLength.HasValue)
            {
                result = result.Skip(startRow.Value).Take(pageLength.Value);
            }

            return result.ToList();
        }

        public virtual Task<IEnumerable<TEntity>> QueryPageAsync(int? startRow, int? pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool includeProperties = false)
        {
            return QueryPageAsync(startRow, pageLength, filter, orderBy, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task<IEnumerable<TEntity>> QueryPageAsync(int? startRow, int? pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            if (orderBy == null)
            {
                orderBy = this.DefaultOrderBy.Expression;
            }

            var result = QueryDb(filter, orderBy, includes);
            if (startRow.HasValue && pageLength.HasValue)
            {
                result = result.Skip(startRow.Value).Take(pageLength.Value);
            }

            return await result.ToListAsync();
        }

        public virtual async Task AddRangeAsync(TEntity[] entities)
        {
            if (entities == null)
            {
                throw new InvalidOperationException("Unable to add a null entities to the repository.");
            }

            dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            await dbSet.AddRangeAsync(entities);

            dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public virtual void Remove(TEntity entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Deleted;
            dbSet.Remove(entity);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.AnyAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, bool includeProperties = false)
        {
            return this.CountAsync(filter, includeProperties ? this.IncludePropertiesForList : null);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            var query = this.QueryDb(filter, null, includes);

            return await query.CountAsync();
        }

        public void SetUnchanged(TEntity entity)
        {
            dbContext.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

        public string GetSql(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToSql();
        }

        protected IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(ViewSql))
            {
                query = query.FromSql(ViewSql);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            #if DEBUG
            var sqlDebug = query.ToSql();
            #endif

            return query;
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
