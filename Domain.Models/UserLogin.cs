namespace Prizma.Domain.Models
{
    using System;

    /// <summary>
    /// The user login class provides the model for login providers and their assigned key.
    /// </summary>
    public class UserLogin : DomainBase<Guid>
    {
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public string ProviderKey { get; set; }
    }
}