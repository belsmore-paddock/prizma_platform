namespace Prizma.Domain.Services.Tests.Exceptions
{
    using System;

    using Prizma.Domain.Models;
    using Prizma.Domain.Services.Exceptions;

    using Xunit;

    /// <summary>
    /// The entity does not exist exception test.
    /// </summary>
    public class EntityDoesNotExistExceptionTest
    {
        /// <summary>
        /// The exception being tested.
        /// </summary>
        private readonly EntityDoesNotExistException exception;

        /// <summary>
        /// The id value being verified.
        /// </summary>
        private readonly Guid id = Guid.NewGuid();

        /// <summary>
        /// The type value being verified.
        /// </summary>
        private readonly Type type = typeof(Project);

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistExceptionTest"/> class.
        /// </summary>
        public EntityDoesNotExistExceptionTest()
        {
            this.exception = new EntityDoesNotExistException(this.type, this.id);
        }

        [Fact(DisplayName = "Type property when value passed during construction returns provided value.")]
        public void TypePropertyWhenValuesPassedDuringConstructionReturnsProvidedValue()
        {
            Assert.Equal(this.type, this.exception.Type);
        }

        [Fact(DisplayName = "Id property when value passed during construction returns provided value.")]
        public void IdPropertyWhenValuesPassedDuringConstructionReturnsProvidedValue()
        {
            Assert.Equal(this.id, this.exception.Id);
        }
    }
}