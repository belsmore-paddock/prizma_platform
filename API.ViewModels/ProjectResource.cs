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
        /// Gets or sets the description.
        /// </summary>
        [Attr("description")]
        [Required]
        public string Description { get; set; }
    }
}