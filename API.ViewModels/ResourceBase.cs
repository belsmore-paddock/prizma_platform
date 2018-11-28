namespace Prizma.API.ViewModels
{
    using System;

    using JsonApiDotNetCore.Models;

    /// <summary>
    /// The resource base.
    /// </summary>
    public abstract class ResourceBase : IIdentifiable<Guid>
    {
        /// <summary>
        /// Gets or sets the string id value.
        /// </summary>
        public string StringId
        {
            get
            {
                var returnValue = string.Empty;

                if (this.Id != Guid.Empty)
                {
                    returnValue = this.Id.ToString();
                }

                return returnValue;
            }

            set => this.Id = Guid.Parse(value);
        }

        /// <summary>
        /// Gets or sets the unique id guid value.
        /// </summary>
        public Guid Id { get; set; }
    }
}