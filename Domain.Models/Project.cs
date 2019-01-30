namespace Prizma.Domain.Models
{
    using System;

    using Builders;

    /// <inheritdoc />
    /// <summary>
    /// The project model provides the base aggregate for a single project.
    /// </summary>
    public class Project : DomainBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="builder">
        /// The builder constructing the current project.
        /// </param>
        public Project(ProjectBuilder builder)
        {
            this.Id = builder.Id;
            this.Description = builder.Description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        protected Project()
        {
            // This is required for Entity Framework.
        }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        public string Description { get; protected set; }
    }
}