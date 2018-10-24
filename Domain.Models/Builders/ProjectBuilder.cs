namespace Prizma.Domain.Models.Builders
{
    using System;

    using FluentValidation;

    using Prizma.Domain.Models.Validations.Construction;

    /// <summary>
    /// The project builder.
    /// </summary>
    public class ProjectBuilder : BuilderBase<Project>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        protected internal Guid Id { get; protected set; }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        protected internal string Description { get; protected set; }

        /// <summary>
        /// Gets the validator for the current target domain model.
        /// </summary>
        protected override IValidator<Project> Validator { get; } = new ProjectConstructionValidator();

        /// <summary>
        /// Performs a creation of the project entity.
        /// </summary>
        /// <returns>
        /// The <see cref="Project"/>.
        /// </returns>
        public override Project DoBuild()
        {
            return new Project(this);
        }

        /// <summary>
        /// Builds new Project with a random Id.
        /// </summary>
        /// <returns>
        /// The <see cref="ProjectBuilder"/>.
        /// </returns>
        public ProjectBuilder WithId()
        {
            var id = Guid.NewGuid();
            return this.WithId(id);
        }

        /// <summary>
        /// Builds a new Project with the provided id.
        /// </summary>
        /// <param name="id">
        /// The id to use as the id of the constructed project.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectBuilder"/>.
        /// </returns>
        public ProjectBuilder WithId(Guid id)
        {
            this.Id = id;
            return this;
        }

        /// <summary>
        /// Builds a new project with the provided description.
        /// </summary>
        /// <param name="description">
        /// The description to use with the built project.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectBuilder"/>.
        /// </returns>
        public ProjectBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }
    }
}