namespace Prizma.Domain.Models
{
    using System;

    /// <summary>
    /// The user claim class provides a model for available system claims assignable to a user.
    /// </summary>
    public class UserClaim : DomainBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaim"/> class.
        /// </summary>
        protected UserClaim()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaim"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public UserClaim(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}