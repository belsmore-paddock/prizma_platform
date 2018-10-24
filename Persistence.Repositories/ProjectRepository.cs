namespace Prizma.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Prizma.Domain.Models;
    using Prizma.Domain.Repositories;

    /// <summary>
    /// The project repository.
    /// </summary>
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ProjectRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Returns a boolean indicating whether the provided entity id already exists.
        /// </summary>
        /// <param name="id">
        /// The id being checked.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Exists(Guid id)
        {
            return this.Context.Query<Project>().Any(p => p.Id == id);
        }
    }
}
