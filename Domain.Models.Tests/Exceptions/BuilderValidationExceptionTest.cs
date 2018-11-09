namespace Prizma.Domain.Models.Tests.Exceptions
{
    using System.Collections.Generic;

    using FluentValidation.Results;

    using Prizma.Domain.Models.Builders;

    using Xunit;

    /// <summary>
    /// The builder validation exception test.
    /// </summary>
    public class BuilderValidationExceptionTest
    {
        /// <summary>
        /// The exception being used as the SUT.
        /// </summary>
        private readonly BuilderValidationException exception;

        /// <summary>
        /// The message being validated with the exception.
        /// </summary>
        private readonly string message = "Hello! I am an error message";

        /// <summary>
        /// The validation failures being validated with the exception.
        /// </summary>
        private readonly IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderValidationExceptionTest"/> class.
        /// </summary>
        public BuilderValidationExceptionTest()
        {
            this.exception = new BuilderValidationException(this.message, this.validationFailures);
        }

        [Fact(DisplayName = "Message property when value passed during construction returns provided value.")]
        public void MessagePropertyWhenValuePassedDuringConstructionReturnsProvidedValue()
        {
            Assert.Equal(this.message, this.exception.Message);
        }

        [Fact(DisplayName = "Errors property when values passed during construction returns values.")]
        public void ErrorsPropertyWhenValuesPassedDuringConstructionReturnsValues()
        {
            Assert.Equal(this.validationFailures, this.exception.Errors);
        }
    }
}