namespace Prizma.API.Services.Interfaces
{
    using System;

    using JsonApiDotNetCore.Services;

    using Prizma.API.ViewModels;

    /// <summary>
    /// The User Resource Service is an application service that handles controller-level logic for Project resources.
    /// </summary>
    public interface IUserResourceService : ISynchronousResourceService<UserResource>, IResourceService<UserResource, Guid>
    {
    }
}