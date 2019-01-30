namespace Prizma.Domain.Models
{
    using System;
    using System.Linq;

    using Prizma.Domain.Models.Attributes;

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
        [NonUpdateable]
        public virtual T Id { get; protected set; }

        /// <summary>
        /// Gets or sets the created at timestamp.
        /// </summary>
        [NonUpdateable]
        public virtual DateTime? CreatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the updated at timestamp.
        /// </summary>
        [NonUpdateable]
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

        /// <summary>
        /// The update from.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public virtual void UpdateFrom(DomainBase<T> entity)
        {
            var updateableProperties = 
                this.GetType()
                    .GetProperties()
                    .Where(e => !e.IsDefined(typeof(NonUpdateableAttribute), true));

            foreach (var property in updateableProperties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(entity, null), null);
                }
            }
        }
    }
}