namespace Prizma.API.Services.Profiles
{
    using AutoMapper;

    using Domain.Models;

    using Prizma.API.ViewModels;

    /// <summary>
    /// The project profile defines the auto mapper mappings between the domain Project model and Project Resource.
    /// </summary>
    public class ProjectProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectProfile"/> class.
        /// </summary>
        public ProjectProfile()
        {
            // Outgoing
            this.CreateMap<Project, ProjectResource>();

            // Incoming
            this.CreateMap<ProjectResource, Project>();
        }
    }
}