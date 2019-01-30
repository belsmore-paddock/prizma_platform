namespace Prizma.Domain.Repositories
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Prizma.Domain.Models;

    /// <summary>
    /// The Repository interface defines methods for accessing, updating, and deleting entity data.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Type of domain entity implementation is interacting with.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// Type of ID used for the entity.
    /// </typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : DomainBase<TKey>
    {
        /// <summary>
        /// Finds a single entity belonging to the supplied id from the persistence store.
        /// </summary>
        /// <param name="id">
        /// The id of the entity being retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity FindById(TKey id);

        /// <summary>
        /// Finds a single entity belonging to the supplied id asynchronously from the persistence store.
        /// </summary>
        /// <param name="id">
        /// The id of the entity being retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> returning an instance of <see cref="TEntity"/>.
        /// </returns>
        Task<TEntity> FindByIdAsync(TKey id);

        /// <summary>
        /// Adds a new entity to the persistence store.
        /// </summary>
        /// <param name="entity">
        /// The entity being added.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/> created.
        /// </returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a new entity asynchronously to the persistence store.
        /// </summary>
        /// <param name="entity">
        /// The entity being added.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> returning the <see cref="TEntity"/> created.
        /// </returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the persistence store.
        /// </summary>
        /// <param name="entity">
        /// The entity being updated.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity asynchronously in the persistence store.
        /// </summary>
        /// <param name="entity">
        /// The entity being updated.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> returning <see cref="TEntity"/> updated.
        /// </returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes the existing entity from the persistence store.
        /// </summary>
        /// <param name="entity">
        /// The entity being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> indicating that the entity has been deleted or not.
        /// </returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Deletes the existing entity asynchronously from the persistence store.
        /// </summary>
        /// <param name="entity">
        /// The entity being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> returning a <see cref="bool"/> indicating that the entity has been deleted or not.
        /// </returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Lists all entities in the persisted data store.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/> of entities.
        /// </returns>
        IList<TEntity> List();

        /// <summary>
        /// Lists all entities asynchronously in the persisted data store.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/> returning a <see cref="IList"/> of entities.
        /// </returns>
        Task<IList<TEntity>> ListAsync();
    }
}