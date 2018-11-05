namespace Prizma.Domain.Models.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Results;

    using Prizma.Domain.Models.Interfaces;

    /// <summary>
    /// The builder base containing the common methods used for the Builder implementations.
    /// </summary>
    /// <typeparam name="T">
    /// Domain Base entity being constructed.
    /// </typeparam>
    public abstract class BuilderBase<T> : IBuilder<T> where T : DomainBase<Guid>
    {
        /// <summary>
        /// Gets the validator.
        /// </summary>
        protected abstract IValidator<T> Validator { get; }

        /// <summary>
        /// Performs the build operation which constructs and validates the resulting entity.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T DoBuild();

        /// <summary>
        /// Builds a new Project model.
        /// </summary>
        /// <returns>
        /// The <see cref="Project"/>.
        /// </returns>
        public virtual T Build()
        {
            var model = this.DoBuild();
            var errors = this.Validate(model);
            if (errors.Any())
            {
                throw new BuilderValidationException($"Validation failed during construction of domain model of type: {typeof(T)}.", errors);
            }

            return model;
        }

        /// <summary>
        /// Performs the validation of the constructed model.
        /// </summary>
        /// <param name="target">
        /// The target entity being validated.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/> of validation failures.
        /// </returns>
        protected virtual IList<ValidationFailure> Validate(T target)
        {
            var results = this.Validator.Validate(target);
            return results.Errors;
        }
    }
}