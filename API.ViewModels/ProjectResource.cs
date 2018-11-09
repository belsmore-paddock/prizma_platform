namespace Prizma.API.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using JsonApiDotNetCore.Models;

    /// <summary>
    /// The project resource defines the API representation of a project.
    /// </summary>
    public class ProjectResource : ResourceBase
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        [Attr("description")]
        [Required]
        public string Description { get; private set; } // Should only be set via mapping.
    }
}