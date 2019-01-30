namespace Prizma.Domain.Models.Validations.Construction
{
    using FluentValidation;

    /// <summary>
    /// The user construction validator. This provides a validator for the initial user construction
    /// of a user model.
    /// </summary>
    public class UserConstructionValidator : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserConstructionValidator"/> class.
        /// </summary>
        public UserConstructionValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.Email).NotEmpty();
            this.RuleFor(x => x.Password).NotEmpty();
        }
    }
}