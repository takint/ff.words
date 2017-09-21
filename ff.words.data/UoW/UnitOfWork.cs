namespace ff.words.data.UoW
{
    using ff.words.data.Common;
    using ff.words.data.Context;
    using ff.words.data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FFWordsContext _context;

        private IDbContextTransaction _transaction;

        public UnitOfWork(FFWordsContext context)
        {
            _context = context;
        }

        public DbActionResponse Commit()
        {
            try
            {
                var rowsAffected = _context.SaveChanges();
                return new DbActionResponse(rowsAffected > 0);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public async Task<DbActionResponse> CommitAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();
                return new DbActionResponse(rowsAffected > 0);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.GetDbTransaction().Dispose();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _transaction.GetDbTransaction().Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}