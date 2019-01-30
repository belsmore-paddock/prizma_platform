namespace Prizma.Domain.Repositories
{
    using System;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// The find repository.
        /// </summary>
        /// <typeparam name="T">
        /// Repository type being retrieved.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T FindRepository<T>();

        /// <summary>
        /// Begins a transaction for the Unit of Work.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits any pending transactions for the Unit of Work.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Performs a rollback on the pending transaction.
        /// </summary>
        void RollbackTransaction();
    }
}