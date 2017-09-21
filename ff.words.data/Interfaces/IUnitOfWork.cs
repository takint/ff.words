namespace ff.words.data.Interfaces
{
    using ff.words.data.Common;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Unit of Work Interface
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        DbActionResponse Commit();

        Task<DbActionResponse> CommitAsync();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }}
