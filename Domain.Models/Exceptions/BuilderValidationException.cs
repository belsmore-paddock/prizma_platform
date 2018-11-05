namespace Prizma.Domain.Models.Builders
{
    using System;
    using System.Collections.Generic;
    using FluentValidation.Results;

    public class BuilderValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="errors">
        /// The errors.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public BuilderValidationException(string message, IList<ValidationFailure> errors) : base(message)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets the errors associated with this exception.
        /// </summary>
        public IList<ValidationFailure> Errors { get; }
    }
}