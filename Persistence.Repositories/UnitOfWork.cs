namespace Prizma.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Prizma.Domain.Repositories;

    /// <summary>
    /// The unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        /// The repositories.
        /// </summary>
        private readonly IDictionary<Type, Type> repositories = new Dictionary<Type, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">
        /// The database context.
        /// </param>
        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;

            this.repositories.Add(typeof(IProjectRepository), typeof(ProjectRepository));
        }

        /// <summary>
        /// The find repository.
        /// </summary>
        /// <typeparam name="T">
        /// Repository type being returned.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T FindRepository<T>()
        {
            T repository = default(T);

            // TODO: Not crazy how this works. Better solution?
            this.repositories.TryGetValue(typeof(T), out var repositoryType);

            if (repositoryType != null)
            {
                repository = (T)Activator.CreateInstance(repositoryType, this.context);
            }

            return repository;
        }

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this.context.Database.BeginTransaction(); // Technically EF already implements UoW, but no guarantee EF will always be used.
        }

        /// <summary>
        /// Commits a transaction.
        /// </summary>
        public void CommitTransaction()
        {
            this.context.SaveChanges(); // This should automatically commit the transaction
        }

        /// <summary>
        /// The dispose method.
        /// </summary>
        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}