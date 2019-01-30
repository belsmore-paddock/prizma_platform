namespace Prizma.API.Services.Tests.Implementations
{
    using System;
    using System.Collections.Generic;

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

        /// <summary>
        /// Verifies the expected behavior when the update method is called with a valid resource.
        /// The expected result is to receive the updated resource.
        /// </summary>
        [Fact(DisplayName = "Update with a valid project resource returns an updated resource.")]
        public void UpdateWithExistingProjectResourceReturnsUpdatedResource()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedResource = new ProjectResource { Id = id, Description = "Update me!" };
            var createdResource = new ProjectResource();

            var returnProject = new ProjectBuilder().WithId().WithDescription("Hello.").Build();
            returnProject.UpdateTimeStamps();

            this.mockProjectService.Setup(p => p.Update(It.IsAny<Project>())).Returns(returnProject);
            this.mockMapper.Setup(m => m.Map<ProjectResource>(returnProject)).Returns(createdResource);

            // Act
            var resultResource = this.projectResourceService.Update(id, updatedResource);

            // Assert
            Assert.Equal(createdResource, resultResource);
        }

        /// <summary>
        /// Verifies the expected behavior when the update method is called with with an id mismatch.
        /// The expected result is for an argument exception to be raised.
        /// </summary>
        [Fact(DisplayName = "Update with project id mismatch throws an error.")]
        public void UpdateWithProjectIdMismatchThrowsError()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updatedResource = new ProjectResource
                                      {
                                          Id = Guid.NewGuid(), Description = "My Guid doesn't match the resource id"
                                      };

            // Act
            var resultException =
                Assert.Throws<ArgumentException>(() => this.projectResourceService.Update(id, updatedResource));

            // Assert
            Assert.Equal(
                $"Id mismatch. Provided id does not match provided resource id.{Environment.NewLine}Parameter name: id",
                resultException.Message);
        }

        /// <summary>
        /// Verifies the expected behavior when the get method is called with no id provided.
        /// The expected result is a returned list of items.
        /// </summary>
        [Fact(DisplayName = "Get with no parameters returns list of project resources.")]
        public void GetWithNoParametersReturnsListOfProjectResources()
        {
            // Arrange
            var project1 = new ProjectResource
                                      {
                                          Id = Guid.NewGuid(),
                                          Description = "Project Resource Uno"
                                      };

            var project2 = new ProjectResource
                               {
                                   Id = Guid.NewGuid(),
                                   Description = "Project Resource Dos"
                               };

            var projectResources = new List<ProjectResource> { project1, project2 };
            var projectList = new List<Project>();

            this.mockProjectService.Setup(p => p.GetAll()).Returns(projectList);
            this.mockMapper.Setup(m => m.Map<IList<ProjectResource>>(projectList)).Returns(projectResources);

            // Act
            var result = this.projectResourceService.Get();

            // Assert
            Assert.Equal(projectResources, result);
        }

        /// <summary>
        /// Verifies the expected behavior when the get method is called with an id provided.
        /// The expected result is a single project resource.
        /// </summary>
        [Fact(DisplayName = "Get with with parameter returns single project resource.")]
        public void GetWithIdParameterReturnsSingleProjectResource()
        {
            // Arrange
            var projectResource = new ProjectResource
                               {
                                   Id = Guid.NewGuid(),
                                   Description = "Project Resource Uno"
                               };

            var project = new ProjectBuilder()
                .WithId(projectResource.Id)
                .WithDescription(projectResource.Description)
                .Build();

            this.mockProjectService.Setup(p => p.GetById(projectResource.Id)).Returns(project);
            this.mockMapper.Setup(m => m.Map<ProjectResource>(project)).Returns(projectResource);

            // Act
            var result = this.projectResourceService.Get(projectResource.Id);

            // Assert
            Assert.Equal(projectResource, result);
        }

        /// <summary>
        /// Verifies the expected behavior when the delete method is called with an id provided.
        /// The expected result is a boolean value indicating the delete result.
        /// </summary>
        [Fact(DisplayName = "Delete with with parameter returns boolean response.")]
        public void DeleteWithIdParameterReturnsBooleanResponse()
        {
            // Arrange
            var projectResourceId = Guid.NewGuid();
            var updateProject = new ProjectBuilder()
                .WithId()
                .WithDescription("Testing Mr. Update Method.")
                .Build();

            this.mockProjectService.Setup(pr => pr.GetById(projectResourceId)).Returns(updateProject);
            this.mockProjectService.Setup(p => p.Delete(updateProject)).Returns(true);

            // Act
            var result = this.projectResourceService.Delete(projectResourceId);

            // Assert
            Assert.True(result);
        }
    }
}