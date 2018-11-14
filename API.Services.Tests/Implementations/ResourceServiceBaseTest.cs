namespace Prizma.API.Services.Tests.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using JsonApiDotNetCore.Models;

    using Moq;

    using Prizma.API.Services.Implementations;
    using Prizma.API.ViewModels;

    /// <summary>
    /// The resource service base test validates common base behavior for resource services.
    /// </summary>
    public class ResourceServiceBaseTest
    {
        /// <summary>
        /// The mock mapper.
        /// </summary>
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();

        /// <summary>
        /// The service base SUT.
        /// </summary>
        private readonly ResourceServiceBaseTestClass serviceBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceServiceBaseTest"/> class.
        /// </summary>
        public ResourceServiceBaseTest()
        {
            this.serviceBase = new ResourceServiceBaseTestClass(this.mockMapper.Object);
        }

        /// <summary>
        /// The resource service base test class implementing the resource service base. This class is used to perform tests of common behaviors.
        /// </summary>
        private class ResourceServiceBaseTestClass : ResourceServiceBase<ResourceTestClass>
        {
            public ResourceServiceBaseTestClass(IMapper mapper)
                : base(mapper)
            {
            }

            public override Task<ResourceTestClass> CreateAsync(ResourceTestClass entity)
            {
                throw new NotImplementedException();
            }

            public override Task<bool> DeleteAsync(Guid id)
            {
                throw new NotImplementedException();
            }

            public override Task<IEnumerable<ResourceTestClass>> GetAsync()
            {
                throw new NotImplementedException();
            }

            public override Task<ResourceTestClass> GetAsync(Guid id)
            {
                throw new NotImplementedException();
            }

            public override Task<object> GetRelationshipAsync(Guid id, string relationshipName)
            {
                throw new NotImplementedException();
            }

            public override Task<object> GetRelationshipsAsync(Guid id, string relationshipName)
            {
                throw new NotImplementedException();
            }

            public override Task<ResourceTestClass> UpdateAsync(Guid id, ResourceTestClass entity)
            {
                throw new NotImplementedException();
            }

            public override Task UpdateRelationshipsAsync(Guid id, string relationshipName, List<ResourceObject> relationships)
            {
                throw new NotImplementedException();
            }

            public override ResourceTestClass Create(ResourceTestClass resource)
            {
                throw new NotImplementedException();
            }

            public override bool Delete(Guid id)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<ResourceTestClass> Get()
            {
                throw new NotImplementedException();
            }

            public override ResourceTestClass Get(Guid id)
            {
                throw new NotImplementedException();
            }

            public override object GetRelationship(Guid id, string relationshipName)
            {
                throw new NotImplementedException();
            }

            public override object GetRelationships(Guid id, string relationshipName)
            {
                throw new NotImplementedException();
            }

            public override ResourceTestClass Update(Guid id, ResourceTestClass resource)
            {
                throw new NotImplementedException();
            }

            public override void UpdateRelationships(Guid id, string relationshipName, List<ResourceObject> relationships)
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The resource test class extending the resource base class. This is used to support the Resource service base tests.
        /// </summary>
        private class ResourceTestClass : ResourceBase
        {

        }
    }
}
