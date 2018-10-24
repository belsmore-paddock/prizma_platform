namespace Prizma.Domain.Services.Exceptions
{
    using System;

    /// <summary>
    /// The entity does not exist exception. This is thrown in cases where an entity is being updated
    /// but the source entity does not exist.
    /// </summary>
    public class EntityDoesNotExistException : ServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDoesNotExistException"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public EntityDoesNotExistException(Type type, Guid id)
            : base($"Entity Type {type} with Id {id} does not exist.")
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