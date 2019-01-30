namespace Prizma.Domain.Repositories
{
    using System;

    using Prizma.Domain.Models;

    /// <summary>
    /// The Project Repository <see langword="interface"/> handles interaction with the persistence layer.
    /// </summary>
    public interface IProjectRepository : IRepository<Project, Guid>
    {
    }
}