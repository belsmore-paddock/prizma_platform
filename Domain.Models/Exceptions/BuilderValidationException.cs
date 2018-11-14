namespace Prizma.Domain.Models.Exceptions
{
    using System;
    using System.Collections.Generic;

    using FluentValidation.Results;

    /// <summary>
    /// The builder validation exception is thrown when a builder is unable to build a valid object.
    /// </summary>
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