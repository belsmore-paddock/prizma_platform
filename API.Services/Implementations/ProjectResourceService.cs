namespace Prizma.API.Services.Implementations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using JsonApiDotNetCore.Models;

    using Prizma.API.Services.Interfaces;
    using Prizma.API.ViewModels;
    using Prizma.Domain.Models;
    using Prizma.Domain.Models.Builders;
    using Prizma.Domain.Services.Interfaces;

    /// <summary>
    /// The project resource service handles interaction between the UI and the domain layer.
    /// </summary>
    public class ProjectResourceService : ResourceServiceBase<ProjectResource>, IProjectResourceService
    {
        #region Private Fields

        /// <summary>
        /// The project service.
        /// </summary>
        private readonly IProjectService projectService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectResourceService"/> class.
        /// </summary>
        /// <param name="projectService">
        /// The project service instance.
        /// </param>
        /// <param name="mapper">
        /// The auto-mapper instance.
        /// </param>
        public ProjectResourceService(IProjectService projectService, IMapper mapper) : base(mapper)
        {
            this.projectService = projectService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new project resource asynchronously.
        /// </summary>
        /// <param name="resource">
        /// The resource model being persisted.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<ProjectResource> CreateAsync(ProjectResource resource)
        {
            return Task.Run(() => this.Create(resource));
        }

        /// <summary>
        /// Updates a new project resource asynchronously.
        /// </summary>
        /// <param name="id">
        /// The unique id of the model being updated.
        /// </param>
        /// <param name="resource">
        /// The resource model being persisted.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<ProjectResource> UpdateAsync(Guid id, ProjectResource resource)
        {
            return Task.Run(() => this.Update(id, resource));
        }

        /// <summary>
        /// The update relationships async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <param name="relationships">
        /// The relationships.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task UpdateRelationshipsAsync(Guid id, string relationshipName, List<ResourceObject> relationships)
        {
            return Task.Run(() => this.UpdateRelationships(id, relationshipName, relationships));
        }

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<bool> DeleteAsync(Guid id)
        {
            return Task.Run(() => this.Delete(id));
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<IEnumerable<ProjectResource>> GetAsync()
        {
            return Task.Run(() => this.Get());
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<ProjectResource> GetAsync(Guid id)
        {
            return Task.Run(() => this.Get(id));
        }

        /// <summary>
        /// The get relationships async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<object> GetRelationshipsAsync(Guid id, string relationshipName)
        {
            return Task.Run(() => this.GetRelationships(id, relationshipName));
        }

        /// <summary>
        /// The get relationship async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task<object> GetRelationshipAsync(Guid id, string relationshipName)
        {
            return Task.Run(() => this.GetRelationship(id, relationshipName));
        }

        /// <summary>
        /// Creates a new project resource synchronously.
        /// </summary>
        /// <param name="resource">
        /// The resource being created.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectResource"/>.
        /// </returns>
        public override ProjectResource Create(ProjectResource resource)
        {
            return this.DoPerformOperation(() => this.DoCreate(resource));
        }

        /// <summary>
        /// Deletes the resource with the provided id.
        /// </summary>
        /// <param name="id">
        /// The id of the target resource.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Delete(Guid id)
        {
            return this.DoPerformOperation(() => this.projectService.Delete(id));
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public override IEnumerable<ProjectResource> Get()
        {
            return this.DoPerformOperation(this.DoGet);
        }

        /// <summary>
        /// Gets a single resource from the provided entity.
        /// </summary>
        /// <param name="id">
        /// The target resource id being retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectResource"/>.
        /// </returns>
        public override ProjectResource Get(Guid id)
        {
            return this.DoPerformOperation(() => this.DoGetById(id));
        }

        /// <summary>
        /// Gets the relationship values with the provided name.
        /// </summary>
        /// <param name="id">
        /// The target resource id.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object GetRelationship(Guid id, string relationshipName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the specified relationship for the provided resource Id.
        /// </summary>
        /// <param name="id">
        /// The target id.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object GetRelationships(Guid id, string relationshipName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the provided resource.
        /// </summary>
        /// <param name="id">
        /// The id of the resource being updated.
        /// </param>
        /// <param name="resource">
        /// The target resource being updated.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectResource"/>.
        /// </returns>
        public override ProjectResource Update(Guid id, ProjectResource resource)
        {
            if (id != resource.Id)
            {
                throw new ArgumentException("Id mismatch. Provided id does not match provided resource id.", nameof(id));
            }

            return this.DoPerformOperation(() => this.DoUpdate(resource));
        }

        /// <summary>
        /// Updates the provided relationships for the provided resource id.
        /// </summary>
        /// <param name="id">
        /// The id of the resource whose relationships are being updated.
        /// </param>
        /// <param name="relationshipName">
        /// The relationship name.
        /// </param>
        /// <param name="relationships">
        /// The relationship data being updated.
        /// </param>
        public override void UpdateRelationships(Guid id, string relationshipName, List<ResourceObject> relationships)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Does the operation to retrieve a single project resource by id.
        /// </summary>
        /// <param name="id">
        /// The id of the target resource to be retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectResource"/>.
        /// </returns>
        private ProjectResource DoGetById(Guid id)
        {
            var resultEntity = this.projectService.Get(id);
            return this.Mapper.Map<ProjectResource>(resultEntity);
        }

        /// <summary>
        /// Does the operation to do a full get of project resources.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/> of project resources.
        /// </returns>
        private IEnumerable<ProjectResource> DoGet()
        {
            var resultEntities = this.projectService.GetAll();
            var resultResources = this.Mapper.Map<IList<ProjectResource>>(resultEntities);
            return resultResources.AsEnumerable();
        }

        /// <summary>
        /// Does the resource creation.
        /// </summary>
        /// <param name="resource">
        /// The resource being created.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectResource"/>.
        /// </returns>
        private ProjectResource DoCreate(ProjectResource resource)
        {
            var entity = new ProjectBuilder()
                .WithId()
                .WithDescription(resource.Description)
                .Build();

            var resultEntity = this.projectService.Create(entity);
            return this.Mapper.Map<ProjectResource>(resultEntity);
        }

        /// <summary>
        /// Does the resource update.
        /// </summary>
        /// <param name="resource">
        /// The resource being updated.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectResource"/>.
        /// </returns>
        private ProjectResource DoUpdate(ProjectResource resource)
        {
            var entity = this.Mapper.Map<Project>(resource);
            var resultEntity = this.projectService.Update(entity);
            return this.Mapper.Map<ProjectResource>(resultEntity);
        }

        #endregion
    }
}