namespace Prizma.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using Prizma.Domain.Models;
    using Prizma.Persistence.Repositories.Configurations;

    /// <summary>
    /// The entity framework database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// Overrides the base on model creating method to specify schema features for our domain models.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder being intercepted which constructs the model when interacting with the database using EF.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        }
    }
}