namespace Prizma.Domain.Models
{
    using System;

    /// <summary>
    /// The domain base contains common code for all domain models.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the identifier property.
    /// </typeparam>
    public abstract class DomainBase<T>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public T Id { get; protected set; }

        /// <summary>
        /// Gets or sets the created at timestamp.
        /// </summary>
        public virtual DateTime? CreatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the updated at timestamp.
        /// </summary>
        public virtual DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Updates the created and updated at time stamps. Created At will be updated only on initial set.
        /// </summary>
        public virtual void UpdateTimeStamps()
        {
            if (!this.CreatedAt.HasValue)
            {
                this.CreatedAt = DateTime.UtcNow;
            }
            
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}