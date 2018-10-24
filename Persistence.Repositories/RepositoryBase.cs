namespace Prizma.Persistence.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Prizma.Domain.Models;
    using Prizma.Domain.Repositories;

    /// <inheritdoc />
    /// <summary>
    /// The repository base class contains any common methods required for individual implementation of repositories.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class RepositoryBase<T> : IRepository<T, Guid> where T : DomainBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected RepositoryBase(DbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the db context.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// Adds an entity to the persistence context.
        /// </summary>
        /// <param name="entity">
        /// The entity being added.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T Add(T entity)
        {
            return this.Context.Add(entity).Entity;
        }

        /// <summary>
        /// Updates an existing entity to the persistence layer.
        /// </summary>
        /// <param name="entity">
        /// The entity being updated.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T Update(T entity)
        {
            return this.Context.Add(entity).Entity;
        }

        /// <summary>
        /// Deletes an existing entity from the persistence layer.
        /// </summary>
        /// <param name="id">
        /// The id key of the target entity being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Delete(Guid id)
        {
            var entity = this.Context.Find<T>(id);
            this.Context.Remove(entity);
            return true;
        }

        /// <summary>
        /// Returns a list of entities.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public virtual IList<T> List()
        {
            return this.Context.Set<T>().ToList();
        }

        /// <summary>
        /// Finds a single result by id.
        /// </summary>
        /// <param name="id">
        /// The target entity id being retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T FindById(Guid id)
        {
            return this.Context.Find<T>(id);
        }
    }
}