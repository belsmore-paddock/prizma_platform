namespace Prizma.Domain.Services.Tests.Implementations
{
    using System;
    using System.Collections.Generic;

    using Moq;

    using Prizma.Domain.Models;
    using Prizma.Domain.Models.Builders;
    using Prizma.Domain.Repositories;
    using Prizma.Domain.Services.Exceptions;
    using Prizma.Domain.Services.Implementations;
    using Prizma.Domain.Services.Interfaces;

    using Xunit;

    /// <summary>
    /// The project service test verifies behavior of the ProjectService implementation.
    /// </summary>
    public class ProjectServiceTest
    {
        /// <summary>
        /// The project service SUT.
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        /// The unit of work mock.
        /// </summary>
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        /// <summary>
        /// The project repository mock.
        /// </summary>
        private readonly Mock<IProjectRepository> projectRepositoryMock = new Mock<IProjectRepository>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectServiceTest"/> class.
        /// </summary>
        public ProjectServiceTest()
        {
            this.unitOfWorkMock.Setup(uow => uow.FindRepository<IProjectRepository>())
                .Returns(this.projectRepositoryMock.Object);
            this.projectService = new ProjectService(this.unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Create method with new entity returns resulting created model.")]
        public void CreateWithNewEntityReturnsCreatedModel()
        {
            // Arrange
            var project = new ProjectBuilder()
                .WithId()
                .WithDescription("Testing Mr. Create Method.")
                .Build();

            this.projectRepositoryMock.Setup(pr => pr.Exists(project.Id)).Returns(false);
            this.projectRepositoryMock.Setup(pr => pr.Add(project)).Returns(project);

            // Act
            var result = this.projectService.Create(project);

            // Assert
            Assert.NotNull(result.CreatedAt);
            Assert.NotNull(result.UpdatedAt);
        }

        [Fact(DisplayName = "Create method when entity already exists throws an exception.")]
        public void CreateWhenEntityAlreadyExistThrowsException()
        {
            // Arrange
            var project = new ProjectBuilder()
                .WithId()
                .WithDescription("Testing Already Created Exception.")
                .Build();

            this.projectRepositoryMock.Setup(pr => pr.Exists(project.Id)).Returns(true);

            // Act
            var exception = Assert.Throws<EntityAlreadyExistsException>(() => this.projectService.Create(project));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.Project with Id {project.Id} already exists.", exception.Message);
        }

        [Fact(DisplayName = "Update method with existing entity returns resulting updated model.")]
        public void UpdateWithExistingEntityReturnsUpdatedModel()
        {
            // Arrange
            var project = new ProjectBuilder()
                .WithId()
                .WithDescription("Testing Mr. Update Method.")
                .Build();

            this.projectRepositoryMock.Setup(pr => pr.Exists(project.Id)).Returns(true);
            this.projectRepositoryMock.Setup(pr => pr.Update(project)).Returns(project);

            // Act
            var result = this.projectService.Update(project);

            // Assert
            Assert.NotNull(result.CreatedAt);
            Assert.NotNull(result.UpdatedAt);
        }

        [Fact(DisplayName = "Update method when entity does not already exists throws an exception.")]
        public void UpdateWhenEntityDoesNotExistThrowsException()
        {
            // Arrange
            var project = new ProjectBuilder()
                .WithId()
                .WithDescription("Testing Update When Doesn't exist.")
                .Build();

            this.projectRepositoryMock.Setup(pr => pr.Exists(project.Id)).Returns(false);

            // Act
            var exception = Assert.Throws<EntityDoesNotExistException>(() => this.projectService.Update(project));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.Project with Id {project.Id} does not exist.", exception.Message);
        }

        [Fact(DisplayName = "Delete method when valid Id is passed returns true.")]
        public void DeleteWhenValidIdIsPassedReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();

            this.projectRepositoryMock.Setup(pr => pr.Delete(id)).Returns(true);

            // Act
            var result = this.projectService.Delete(id);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Delete method when provided Id cannot be deleted returns false.")]
        public void DeleteWhenProvidedIdCannotBeDeletedReturnsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();

            this.projectRepositoryMock.Setup(pr => pr.Delete(id)).Returns(false);

            // Act
            var result = this.projectService.Delete(id);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Get method when provided Id returns result.")]
        public void GetWhenProvidedIdReturnsResult()
        {
            // Arrange
            var project = new ProjectBuilder()
                .WithId()
                .WithDescription("Testing Single Result.")
                .Build();

            var id = project.Id;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(project);

            // Act
            var result = this.projectService.Get(id);

            // Assert
            Assert.Equal(project, result);
        }

        [Fact(DisplayName = "GetAll method returns full result list.")]
        public void GetAllReturnsResultsList()
        {
            // Arrange
            var project1 = new ProjectBuilder()
                .WithId()
                .WithDescription("Result 1")
                .Build();

            var project2 = new ProjectBuilder()
                .WithId()
                .WithDescription("Result 2")
                .Build();

            var list = new List<Project> { project1, project2 };

            this.projectRepositoryMock.Setup(pr => pr.List()).Returns(list);

            // Act
            var result = this.projectService.GetAll();

            // Assert
            Assert.Equal(list, result);
        }
    }
}