namespace Prizma.Domain.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using Prizma.Domain.Models;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="T">
    /// Domain model type.
    /// </typeparam>
    /// <typeparam name="TP">
    /// Type of the model identifier.
    /// </typeparam>
    public interface IRepository<T, in TP> where T : DomainBase<TP>
    {
        /// <summary>
        /// Finds a single domain model belonging to the provided id.
        /// </summary>
        /// <param name="id">
        /// The entity id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T FindById(TP id);

        /// <summary>
        /// Adds a single domain model to the persistence layer.
        /// </summary>
        /// <param name="entity">
        /// The entity being persisted.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Add(T entity);

        /// <summary>
        /// Updates a single existing persisted entity to the persistence layer.
        /// </summary>
        /// <param name="entity">
        /// The entity being updated.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Update(T entity);

        /// <summary>
        /// Deletes an existing entity with the provided id from the persistence layer.
        /// </summary>
        /// <param name="id">
        /// The target entity id being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Delete(TP id);

        /// <summary>
        /// Returns a full list of entities.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<T> List();
    }
}