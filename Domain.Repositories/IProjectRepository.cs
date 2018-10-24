namespace Prizma.Domain.Repositories
{
    using System;

    using Prizma.Domain.Models;

    /// <summary>
    /// The Project Repository interface handles interaction with the persistence layer.
    /// </summary>
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        /// <summary>
        /// Indicates whether an entity already exists for the provided id.
        /// </summary>
        /// <param name="id">
        /// The entity id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> indicating whether the provided project id already exists.
        /// </returns>
        /// TODO: Move to IRepository?
        bool Exists(Guid id);
    }
}