namespace Prizma.API.Services.Adapters
{
    using AutoMapper;

    using JsonApiDotNetCore.Models;

    /// <summary>
    /// The auto mapper adapter for use with the JSON Api implementation.
    /// </summary>
    public class AutoMapperAdapter : IResourceMapper
    {
        #region Private Fields

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperAdapter"/> class.
        /// </summary>
        /// <param name="mapper">
        /// The mapper instance.
        /// </param>
        public AutoMapperAdapter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Maps the provided object to the destination type.
        /// </summary>
        /// <param name="source">
        /// The source object.
        /// </param>
        /// <typeparam name="TDestination">
        /// Destination type for the source model.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TDestination"/>.
        /// </returns>
        public TDestination Map<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps the provided object of the provided source to the destination type.
        /// </summary>
        /// <param name="source">
        /// The source object.
        /// </param>
        /// <typeparam name="TSource">
        /// Source model being mapped.
        /// </typeparam>
        /// <typeparam name="TDestination">
        /// Destination type for the source model.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TDestination"/>.
        /// </returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return this.mapper.Map<TSource, TDestination>(source);
        }

        #endregion
    }
}