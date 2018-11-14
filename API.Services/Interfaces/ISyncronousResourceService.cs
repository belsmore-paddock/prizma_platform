namespace Prizma.API.Services.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using JsonApiDotNetCore.Models;

    using Prizma.API.ViewModels;

    /// <summary>
    /// The Synchronous Resource Service interface provides basic implementations of the methods used by the IResource service.
    /// </summary>
    /// <typeparam name="T">
    /// Resource class type.
    /// </typeparam>
    public interface ISynchronousResourceService<T> where T : ResourceBase
    {
        /// <summary>
        /// Creates a new resource.
        /// </summary>
        /// <param name="resource">
        /// The resource being created.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Create(T resource);

        /// <summary>
        /// Deletes an existing resource.
        /// </summary>
        /// <param name="id">
        /// The id of the resource being deleted.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Delete(Guid id);

        /// <summary>
        /// Gets an enumerable of all resources.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Gets a single resource belonging to the provided id.
        /// </summary>
        /// <param name="id">
        /// The id of the resource being retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Get(Guid id);

        /// <summary>
        /// Gets the specified relationship belonging to the resource id.
        /// </summary>
        /// <param name="id">
        /// The id of the base resource.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetRelationship(Guid id, string relationshipName);

        /// <summary>
        /// Gets the specified relationship(s) belonging to the resource id.
        /// </summary>
        /// <param name="id">
        /// The id of the base resource.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetRelationships(Guid id, string relationshipName);

        /// <summary>
        /// Updates an existing resource belonging to the provided id.
        /// </summary>
        /// <param name="id">
        /// The target resource id being updated.
        /// </param>
        /// <param name="resource">
        /// The resource.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Update(Guid id, T resource);

        /// <summary>
        /// Updates the relationship details for the provided base resource id.
        /// </summary>
        /// <param name="id">
        /// The id of the base resource.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <param name="relationships">
        /// The relationship data.
        /// </param>
        void UpdateRelationships(
            Guid id,
            string relationshipName,
            List<ResourceObject> relationships);
    }
}
