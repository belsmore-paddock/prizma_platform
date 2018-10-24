namespace Prizma.Domain.Services.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Prizma.Domain.Models;

    /// <summary>
    /// The Project Service interface defines the contract for the domain business logic for project entities.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Creates a new project entity.
        /// </summary>
        /// <param name="entity">
        /// The entity being persisted.
        /// </param>
        /// <returns>
        /// The <see cref="Project"/>.
        /// </returns>
        Project Create(Project entity);

        /// <summary>
        /// Updates an existing project entity.
        /// </summary>
        /// <param name="entity">
        /// The entity being updated.
        /// </param>
        /// <returns>
        /// The <see cref="Project"/>.
        /// </returns>
        Project Update(Project entity);

        /// <summary>
        /// Returns all project entities persisted.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<Project> GetAll();

        /// <summary>
        /// Gets a single project entity matching the provided id.
        /// </summary>
        /// <param name="id">
        /// The target id of the persisted entity.
        /// </param>
        /// <returns>
        /// The <see cref="Project"/>.
        /// </returns>
        Project Get(Guid id);

        /// <summary>
        /// Deletes the entity with the provided id.
        /// </summary>
        /// <param name="id">
        /// The target id of the persisted entity being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Delete(Guid id);
    }
}