namespace Prizma.API.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using JsonApiDotNetCore.Models;
    using JsonApiDotNetCore.Services;

    using Prizma.API.Services.Interfaces;
    using Prizma.API.ViewModels;

    /// <summary>
    /// The resource service base.
    /// </summary>
    /// <typeparam name="T">
    /// Resource base class type.
    /// </typeparam>
    public abstract class ResourceServiceBase<T> : ResourceBase, ISynchronousResourceService<T>, IResourceService<T, Guid> where T : class, IIdentifiable<Guid>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceServiceBase{T}"/> class.
        /// </summary>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        protected ResourceServiceBase(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the auto mapper instance.
        /// </summary>
        protected IMapper Mapper { get; }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public abstract Task<T> CreateAsync(T entity );

        /// <inheritdoc />
        public abstract Task<bool> DeleteAsync(Guid id);

        /// <inheritdoc />
        public abstract Task<IEnumerable<T>> GetAsync();

        /// <inheritdoc />
        public abstract Task<T> GetAsync(Guid id);

        /// <inheritdoc />
        public abstract Task<object> GetRelationshipAsync(Guid id, string relationshipName);

        /// <inheritdoc />
        public abstract Task<object> GetRelationshipsAsync(Guid id, string relationshipName);

        /// <inheritdoc />
        public abstract Task<T> UpdateAsync(Guid id, T entity);

        /// <inheritdoc />
        public abstract Task UpdateRelationshipsAsync(
            Guid id,
            string relationshipName,
            List<ResourceObject> relationships);

        /// <inheritdoc />
        public abstract T Create(T resource);

        /// <inheritdoc />
        public abstract bool Delete(Guid id);

        /// <inheritdoc />
        public abstract IEnumerable<T> Get();

        /// <inheritdoc />
        public abstract T Get(Guid id);

        /// <inheritdoc />
        public abstract object GetRelationship(Guid id, string relationshipName);

        /// <inheritdoc />
        public abstract object GetRelationships(Guid id, string relationshipName);

        /// <inheritdoc />
        public abstract T Update(Guid id, T resource);

        /// <inheritdoc />
        public abstract void UpdateRelationships(Guid id, string relationshipName, List<ResourceObject> relationships);

        #endregion

        #region Protected Methods

        /// <summary>
        /// Performs the provided function operation with centralized error handling.
        /// </summary>
        /// <param name="performAction">
        /// The action to be performed.
        /// </param>
        /// <typeparam name="TR">
        /// Result type of the function.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TR"/>.
        /// </returns>
        protected TR DoPerformOperation<TR>(Func<TR> performAction)
        {
            try
            {
                return performAction.Invoke();
            }
            catch (Exception exception)
            {
                // TODO: Log here
                Console.WriteLine(exception);
                throw;
            }
        }

        #endregion
    }
}