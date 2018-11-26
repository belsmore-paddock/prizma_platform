namespace Prizma.API.Services.Tests.Implementations
{
    using AutoMapper;

    using Moq;

    using Prizma.API.Services.Implementations;
    using Prizma.API.Services.Interfaces;
    using Prizma.API.ViewModels;
    using Prizma.Domain.Models;
    using Prizma.Domain.Models.Builders;
    using Prizma.Domain.Services.Interfaces;

    using Xunit;

    /// <summary>
    /// The project resource service test.
    /// </summary>
    public class ProjectResourceServiceTest
    {
        /// <summary>
        /// The project resource service representing the SUT.
        /// </summary>
        private readonly IProjectResourceService projectResourceService;

        /// <summary>
        /// The mock project service.
        /// </summary>
        private readonly Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

        /// <summary>
        /// The mock mapper.
        /// </summary>
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectResourceServiceTest"/> class.
        /// </summary>
        public ProjectResourceServiceTest()
        {
            this.projectResourceService = new ProjectResourceService(this.mockProjectService.Object, this.mockMapper.Object);
        }

        /// <summary>
        /// Verifies the expected behavior when the create method is called with a valid resource.
        /// The expected result is to receive the created resource.
        /// </summary>
        [Fact(DisplayName = "Create with a valid project resource returns a created resource.")]
        public void CreateWithValidProjectResourceReturnsCreatedResource()
        {
            // Arrange
            var createResource = new ProjectResource { Description = "Hi." };
            var createdResource = new ProjectResource();

            var returnProject = new ProjectBuilder().WithId().WithDescription("Hello.").Build();
            returnProject.UpdateTimeStamps();

            this.mockProjectService.Setup(p => p.Create(It.IsAny<Project>())).Returns(returnProject);
            this.mockMapper.Setup(m => m.Map<ProjectResource>(returnProject)).Returns(createdResource);

            // Act
            var resultResource = this.projectResourceService.Create(createResource);

            // Assert
            Assert.Equal(createdResource, resultResource);
        }
    }
}