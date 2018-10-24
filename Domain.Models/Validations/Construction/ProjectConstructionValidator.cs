namespace Prizma.Domain.Models.Validations.Construction
{
    using FluentValidation;

    /// <summary>
    /// The project construction validator. This provides a validator for the initial project construction
    /// of a Project model.
    /// </summary>
    public class ProjectConstructionValidator : AbstractValidator<Project>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectConstructionValidator"/> class.
        /// </summary>
        public ProjectConstructionValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Description).NotEmpty();
        }
    }
}