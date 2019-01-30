namespace Prizma.API.ViewModels
{
    using System;

    using JsonApiDotNetCore.Models;

    using Microsoft.AspNetCore.Identity;

    /// <inheritdoc />
    public class UserResource : IdentityUser<Guid>, IIdentifiable<Guid>
    {
        /// <summary>
        /// Gets or sets the string id.
        /// </summary>
        public string StringId { get; set; }
    }
}
