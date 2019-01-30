namespace Prizma.Domain.Services.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Prizma.Domain.Models;

    /// <summary>
    /// The User Service <see langword="interface"/> provides the structure to work with user objects.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user entity.
        /// </summary>
        /// <param name="entity">
        /// The entity being persisted.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User Create(User entity);

        /// <summary>
        /// Updates an existing user entity.
        /// </summary>
        /// <param name="entity">
        /// The entity being updated.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User Update(User entity);

        /// <summary>
        /// Returns all user entities persisted.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<User> GetAll();

        /// <summary>
        /// Gets a single user entity matching the provided id.
        /// </summary>
        /// <param name="id">
        /// The target id of the persisted entity.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User GetById(Guid id);

        /// <summary>
        /// Gets a single user entity matching the provided id or providing a default null value if not exists.
        /// </summary>
        /// <param name="id">
        /// The target id of the persisted entity.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        User GetByIdOrDefault(Guid id);

        /// <summary>
        /// Deletes the entity with the provided id.
        /// </summary>
        /// <param name="user">
        /// The target user being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        bool Delete(User user);

        /// <summary>
        /// The bulk create method accepts a set of entries to perform a bulk insert on.
        /// </summary>
        /// <param name="userSet">
        /// The user set to be inserted.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/> of created projects.
        /// </returns>
        IList<User> CreateMany(ISet<User> userSet);
    }
}
