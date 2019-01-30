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
    /// The user resource service handles interaction between the UI and the domain layer.
    /// </summary>
    public class UserResourceService : ResourceServiceBase<UserResource>, IUserResourceService
    {
        #region Private Fields

        /// <summary> 
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserResourceService"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service instance.
        /// </param>
        /// <param name="mapper">
        /// The auto-mapper instance.
        /// </param>
        public UserResourceService(IUserService userService, IMapper mapper) : base(mapper)
        {
            this.userService = userService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new user resource asynchronously.
        /// </summary>
        /// <param name="resource">
        /// The resource model being persisted.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task<UserResource> CreateAsync(UserResource resource)
        {
            return await Task.Run(() => this.Create(resource));
        }

        /// <summary>
        /// Updates a new user resource asynchronously.
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
        public override async Task<UserResource> UpdateAsync(Guid id, UserResource resource)
        {
            return await Task.Run(() => this.Update(id, resource));
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
        public override async Task<bool> DeleteAsync(Guid id)
        {
            return await Task.Run(() => this.Delete(id));
        }

        /// <summary>
        /// The get async method returns a task yielding a list of users.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task<IEnumerable<UserResource>> GetAsync()
        {
            return await Task.Run(() => this.Get());
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
        public override async Task<UserResource> GetAsync(Guid id)
        {
            return await Task.Run(() => this.Get(id));
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
        public override async Task<object> GetRelationshipsAsync(Guid id, string relationshipName)
        {
            return await Task.Run(() => this.GetRelationships(id, relationshipName));
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
        public override async Task<object> GetRelationshipAsync(Guid id, string relationshipName)
        {
            return await Task.Run(() => this.GetRelationship(id, relationshipName));
        }

        /// <summary>
        /// Creates a new user resource synchronously.
        /// </summary>
        /// <param name="resource">
        /// The resource being created.
        /// </param>
        /// <returns>
        /// The <see cref="UserResource"/>.
        /// </returns>
        public override UserResource Create(UserResource resource)
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
            var user = this.userService.GetById(id);
            return this.DoPerformOperation(() => this.userService.Delete(user));
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public override IEnumerable<UserResource> Get()
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
        /// The <see cref="UserResource"/>.
        /// </returns>
        public override UserResource Get(Guid id)
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
        /// The <see cref="UserResource"/>.
        /// </returns>
        public override UserResource Update(Guid id, UserResource resource)
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
        /// Does the operation to retrieve a single user resource by id.
        /// </summary>
        /// <param name="id">
        /// The id of the target resource to be retrieved.
        /// </param>
        /// <returns>
        /// The <see cref="UserResource"/>.
        /// </returns>
        private UserResource DoGetById(Guid id)
        {
            var resultEntity = this.userService.GetById(id);
            return this.Mapper.Map<UserResource>(resultEntity);
        }

        /// <summary>
        /// Does the operation to do a full get of user resources.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/> of user resources.
        /// </returns>
        private IEnumerable<UserResource> DoGet()
        {
            var resultEntities = this.userService.GetAll();
            var resultResources = this.Mapper.Map<IList<UserResource>>(resultEntities);
            return resultResources.AsEnumerable();
        }

        /// <summary>
        /// Does the resource creation.
        /// </summary>
        /// <param name="resource">
        /// The resource being created.
        /// </param>
        /// <returns>
        /// The <see cref="UserResource"/>.
        /// </returns>
        private UserResource DoCreate(UserResource resource)
        {
            var entity = new UserBuilder()
                .WithId()
                .WithUserName(resource.UserName)
                .WithEmail(resource.Email)
                .WithPasswordHash(resource.PasswordHash)
                .WithActive()
                .Build();

            var resultEntity = this.userService.Create(entity);
            return this.Mapper.Map<UserResource>(resultEntity);
        }

        /// <summary>
        /// Does the <paramref name="resource"/> update.
        /// </summary>
        /// <param name="resource">
        /// The resource being updated.
        /// </param>
        /// <returns>
        /// The <see cref="UserResource"/>.
        /// </returns>
        private UserResource DoUpdate(UserResource resource)
        {
            var entity = this.Mapper.Map<User>(resource);
            var resultEntity = this.userService.Update(entity);
            return this.Mapper.Map<UserResource>(resultEntity);
        }

        #endregion
    }
}