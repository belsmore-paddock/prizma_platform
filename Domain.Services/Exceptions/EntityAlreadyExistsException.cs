namespace Prizma.Domain.Services.Exceptions
{
    using System;

    /// <summary>
    /// The entity already exists exception. This is thrown when there is an attempt to create a new instance
    /// of a model, but there already is a model persisted with the same id.
    /// </summary>
    public class EntityAlreadyExistsException : ServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public EntityAlreadyExistsException(Type type, Guid id)
            : base($"Entity Type {type} with Id {id} already exists.")
        {
            this.Type = type;
            this.Id = id;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; }
    }
}