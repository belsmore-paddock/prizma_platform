namespace Prizma.Domain.Models.Tests.Builders
{
    using System;
    using System.Linq;

    using Prizma.Domain.Models.Builders;
    using Prizma.Domain.Models.Exceptions;

    using Xunit;

    /// <summary>
    /// The project builder test cases.
    /// </summary>
    public class ProjectBuilderTest
    {
        /// <summary>
        /// The builder representing the SUT.
        /// </summary>
        private readonly ProjectBuilder builder = new ProjectBuilder();

        [Fact(DisplayName = "Build method when required values are provided produces a new project.")]
        public void BuildWhenAllRequiredValuesProvidedBuildsNewProject()
        {
            // Arrange
            var id = Guid.NewGuid();
            var description = "Hello! I am a description.";

            this.builder.WithId(id);
            this.builder.WithDescription(description);

            // Act
            var project = this.builder.Build();

            // Assert
            Assert.NotNull(project);
            Assert.Equal(id, project.Id);
            Assert.Equal(description, project.Description);
        }

        [Fact(DisplayName = "Build method when non overload Id method is used produces a new project.")]
        public void BuildWhenIdValueNotProvidedWithOverloadMethodBuildsNewProject()
        {
            // Arrange
            var description = "Hello! I am a description.";

            this.builder.WithId();
            this.builder.WithDescription(description);

            // Act
            var project = this.builder.Build();

            // Assert
            Assert.NotNull(project);
            Assert.NotEqual(Guid.Empty, project.Id);
            Assert.Equal(description, project.Description);
        }

        [Fact(DisplayName = "Build method when description is not provided throws validation exception.")]
        public void BuildWhenDescriptionIsNotProvidedThrowsValidationException()
        {
            // Arrange
            var id = Guid.NewGuid();

            this.builder.WithId(id);

            // Act
            var exception = Assert.Throws<BuilderValidationException>(() => this.builder.Build());

            // Assert
            var errorMessage = exception.Errors.Single();

            Assert.NotNull(exception);
            Assert.Equal("Validation failed during construction of domain model of type: Prizma.Domain.Models.Project.", exception.Message);
            Assert.Equal("\'Description\' should not be empty.", errorMessage.ErrorMessage);
            Assert.Equal("Description", errorMessage.PropertyName);
        }

        [Fact(DisplayName = "Build method when id is not provided throws validation exception.")]
        public void BuildWhenIdIsNotProvidedThrowsValidationException()
        {
            // Arrange
            var description = "Hello! I am a description.";

            this.builder.WithDescription(description);

            // Act
            var exception = Assert.Throws<BuilderValidationException>(() => this.builder.Build());

            // Assert
            var errorMessage = exception.Errors.Single();

            Assert.NotNull(exception);
            Assert.Equal("Validation failed during construction of domain model of type: Prizma.Domain.Models.Project.", exception.Message);
            Assert.Equal("\'Id\' should not be empty.", errorMessage.ErrorMessage);
            Assert.Equal("Id", errorMessage.PropertyName);
        }

        [Fact(DisplayName = "Build method when no required parameters are provided throws validation exception.")]
        public void BuildWhenNoRequiredParametersAreProvidedThrowsValidationException()
        {
            // Arrange
            // No build actions done.

            // Act
            var exception = Assert.Throws<BuilderValidationException>(() => this.builder.Build());

            // Assert
            Assert.Equal("Validation failed during construction of domain model of type: Prizma.Domain.Models.Project.", exception.Message);
            Assert.Equal(2, exception.Errors.Count);
            var idError = exception.Errors.Single(e => e.PropertyName.Equals("Id"));
            var descriptionError = exception.Errors.Single(e => e.PropertyName.Equals("Description"));

            Assert.Equal("\'Id\' should not be empty.", idError.ErrorMessage);
            Assert.Equal("Id", idError.PropertyName);

            Assert.Equal("\'Description\' should not be empty.", descriptionError.ErrorMessage);
            Assert.Equal("Description", descriptionError.PropertyName);
        }
    }
}