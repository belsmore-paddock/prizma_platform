namespace Prizma.Domain.Models.Tests.Builders
{
    using System;
    using System.Collections.Generic;

    using FluentValidation;
    using FluentValidation.Results;

    using Moq;

    using Prizma.Domain.Models.Builders;
    using Prizma.Domain.Models.Exceptions;

    using Xunit;

    /// <summary>
    /// The builder base test validates functional behavior of the base builder class.
    /// </summary>
    public class BuilderBaseTest
    {
        /// <summary>
        /// The builder representing the SUT.
        /// </summary>
        private readonly BuilderBaseTestClass builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderBaseTest"/> class.
        /// </summary>
        public BuilderBaseTest()
        {
            this.builder = new BuilderBaseTestClass();
        }

        [Fact(DisplayName = "Builder Build method when valid returns object")]
        public void BuildWhenValidReturnsObject()
        {
            var mockValidator = new Mock<IValidator<DomainBaseTestClass>>();
            var mockResult = new Mock<ValidationResult>();
            mockResult.Setup(r => r.IsValid).Returns(true);

            mockValidator.Setup(v => v.Validate(It.IsAny<DomainBaseTestClass>())).Returns(mockResult.Object);

            var result = this.builder.WithValidator(mockValidator.Object).Build();

            Assert.NotNull(result);
            Assert.IsType<DomainBaseTestClass>(result);
        }

        [Fact(DisplayName = "Builder Build method when not valid returns object")]
        public void BuildWhenNotValidReturnsObject()
        {
            var mockValidator = new Mock<IValidator<DomainBaseTestClass>>();
            
            var errors = new List<ValidationFailure> { new ValidationFailure("any", "Must be awesome") };
            var validationResult = new ValidationResult(errors);

            mockValidator.Setup(v => v.Validate(It.IsAny<DomainBaseTestClass>())).Returns(validationResult);

            var result = Assert.Throws<BuilderValidationException>(() => { this.builder.WithValidator(mockValidator.Object).Build(); });

            Assert.NotNull(result);
            Assert.Equal("Validation failed during construction of domain model of type: Prizma.Domain.Models.Tests.DomainBaseTestClass.", result.Message);
            Assert.Equal(errors, result.Errors);
        }

        /// <summary>
        /// The builder base test class used to test functionality of the base builder.
        /// </summary>
        private class BuilderBaseTestClass : BuilderBase<DomainBaseTestClass>
        {
            /// <summary>
            /// The validator instance used for this test class.
            /// </summary>
            private IValidator<DomainBaseTestClass> validator;

            /// <summary>
            /// The validator used for validation by the base calss.
            /// </summary>
            protected override IValidator<DomainBaseTestClass> Validator => this.validator;

            /// <summary>
            /// Provides a mechanism to to provide a validator object for testing purposes.
            /// </summary>
            /// <param name="validatorInstance">
            /// The validator instance to use for validation behavior.
            /// </param>
            /// <returns>
            /// The <see cref="BuilderBaseTestClass"/>.
            /// </returns>
            public BuilderBaseTestClass WithValidator(IValidator<DomainBaseTestClass> validatorInstance)
            {
                this.validator = validatorInstance;
                return this;
            }

            /// <summary>
            /// Performs the object construction.
            /// </summary>
            /// <returns>
            /// The <see cref="DomainBaseTestClass"/>.
            /// </returns>
            protected override DomainBaseTestClass DoBuild()
            {
                return new DomainBaseTestClass(Guid.Empty);
            }
        }
    }
}
