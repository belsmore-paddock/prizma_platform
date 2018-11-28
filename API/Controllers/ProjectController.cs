namespace Prizma.API.Controllers
{
    using System;

    using JsonApiDotNetCore.Controllers;
    using JsonApiDotNetCore.Services;

    using Prizma.API.ViewModels;

    /// <summary>
    /// The project controller.
    /// </summary>
    public class ProjectController : JsonApiController<ProjectResource, Guid>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="jsonApiContext">
        /// The json api context.
        /// </param>
        /// <param name="resourceService">
        /// The resource service for handling API requests.
        /// </param>
        public ProjectController(
            IJsonApiContext jsonApiContext,
            IResourceService<ProjectResource, Guid> resourceService)
            : base(jsonApiContext, resourceService)
        {
        }

        #endregion
    }
}