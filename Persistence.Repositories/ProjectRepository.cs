namespace Prizma.Persistence.Repositories
{
    using Domain.Models;

    using Microsoft.EntityFrameworkCore;

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
    }
}