namespace Prizma.Domain.Services.Tests.Implementations
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

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
    public class ProjectServiceTests
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
        public ProjectServiceTests()
        {
            this.unitOfWorkMock.Setup(uow => uow.FindRepository<IProjectRepository>())
                .Returns(this.projectRepositoryMock.Object);
            this.projectService = new ProjectService(this.unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Create method with new entity returns resulting created model.")]
        public void CreateWithNewEntityReturnsCreatedModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sourceProjectMock = new Mock<Project>();
            sourceProjectMock.Setup(p => p.Id).Returns(id);
            var sourceProject = sourceProjectMock.Object;

            Project nullProject = null;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(nullProject);
            this.projectRepositoryMock.Setup(pr => pr.Add(sourceProject)).Returns(sourceProject);

            // Act
            var result = this.projectService.Create(sourceProject);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sourceProject, result);
            sourceProjectMock.Verify(p => p.UpdateTimeStamps(), Times.Once);
        }

        [Fact(DisplayName = "Create method when entity already exists throws an exception.")]
        public void CreateWhenEntityAlreadyExistThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var createProjectMock = new Mock<Project>();
            createProjectMock.Setup(p => p.Id).Returns(id);
            var createProject = createProjectMock.Object;

            var updateProject = new ProjectBuilder().WithId(id).WithDescription("Created!").Build();

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(updateProject);

            // Act
            var exception = Assert.Throws<EntityAlreadyExistsException>(() => this.projectService.Create(createProject));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.Project with Id {id} already exists.", exception.Message);
        }

        [Fact(DisplayName = "Create method when entity parameter is null throws an exception.")]
        public void CreateWhenEntityParameterIsNullThrowsException()
        {
            // Arrange
            Project project = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => this.projectService.Create(project));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Value cannot be null.{Environment.NewLine}Parameter name: entity", exception.Message);
        }

        [Fact(DisplayName = "Update method with existing entity returns resulting updated model.")]
        public void UpdateWithExistingEntityReturnsUpdatedModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sourceProjectMock = new Mock<Project>();
            sourceProjectMock.Setup(p => p.Id).Returns(id);
            var sourceProject = sourceProjectMock.Object;

            var updateProjectMock = new Mock<Project>();
            var updateProject = updateProjectMock.Object;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(updateProject);
            this.projectRepositoryMock.Setup(pr => pr.Update(updateProject)).Returns(updateProject);

            // Act
            var result = this.projectService.Update(sourceProject);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateProject, result);

            updateProjectMock.Verify(p => p.UpdateFrom(sourceProject), Times.Once);
            updateProjectMock.Verify(p => p.UpdateTimeStamps(), Times.Once);
        }

        [Fact(DisplayName = "Update method when entity does not already exists throws an exception.")]
        public void UpdateWhenEntityDoesNotExistThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var projectMock = new Mock<Project>();
            projectMock.Setup(p => p.Id).Returns(id);
            var project = projectMock.Object;

            Project nullProject = null;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(nullProject);

            // Act
            var exception = Assert.Throws<EntityDoesNotExistException>(() => this.projectService.Update(project));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.Project with Id {id} does not exist.", exception.Message);
        }

        [Fact(DisplayName = "Update method when entity parameter is null throws an exception.")]
        public void UpdateWhenEntityParameterIsNullThrowsException()
        {
            // Arrange
            Project project = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => this.projectService.Update(project));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Value cannot be null.{Environment.NewLine}Parameter name: entity", exception.Message);
        }

        [Fact(DisplayName = "Delete method when valid Id is passed returns true.")]
        public void DeleteWhenValidIdIsPassedReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var projectMock = new Mock<Project>();
            projectMock.Setup(p => p.Id).Returns(id);
            var project = projectMock.Object;

            var existingProjectMock = new Mock<Project>();
            var existingProject = existingProjectMock.Object;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(existingProject);
            this.projectRepositoryMock.Setup(pr => pr.Delete(existingProject)).Returns(true);

            // Act
            var result = this.projectService.Delete(project);

            // Assert
            Assert.True(result);

            this.unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            this.unitOfWorkMock.Verify(uow => uow.CommitTransaction());
        }

        [Fact(DisplayName = "Delete method when provided Id cannot be deleted returns false.")]
        public void DeleteWhenProvidedIdCannotBeDeletedReturnsFalse()
        {
            // Arrange
            var projectMock = new Mock<Project>();
            var project = projectMock.Object;

            this.projectRepositoryMock.Setup(pr => pr.Delete(project)).Returns(false);

            // Act
            var result = this.projectService.Delete(project);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Delete method when provided parameter is null throws exception.")]
        public void DeleteWhenProvidedParameterIsNullThrowsException()
        {
            // Arrange
            Project project = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => this.projectService.Delete(project));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Value cannot be null.{Environment.NewLine}Parameter name: project", exception.Message);
        }

        [Fact(DisplayName = "Get by id when provided Id returns result.")]
        public void GetByIdWhenProvidedIdReturnsResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var projectMock = new Mock<Project>();
            var project = projectMock.Object;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(project);

            // Act
            var result = this.projectService.GetById(id);

            // Assert
            Assert.Equal(project, result);
        }

        [Fact(DisplayName = "Get by id when provided Id does not have an entity throws exception.")]
        public void GetByIdWhenProvidedIdDoesntHaveAnEntityThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            Project project = null;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(project);

            // Act
            var exception = Assert.Throws<EntityDoesNotExistException>(() => this.projectService.GetById(id));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.Project with Id {id} does not exist.", exception.Message);
        }

        [Fact(DisplayName = "Get by id or default when provided Id returns result.")]
        public void GetByIdOrDefaultWhenProvidedIdReturnsResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var projectMock = new Mock<Project>();
            var project = projectMock.Object;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(project);

            // Act
            var result = this.projectService.GetByIdOrDefault(id);

            // Assert
            result.Should().Be(project);
        }

        [Fact(DisplayName = "Get by id or default when provided Id does not have an entity throws exception.")]
        public void GetByIdOrDefaultWhenProvidedIdDoesntHaveAnEntityThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            Project project = null;

            this.projectRepositoryMock.Setup(pr => pr.FindById(id)).Returns(project);

            // Act
            var result = this.projectService.GetByIdOrDefault(id);

            // Assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = "GetAll method returns full result list.")]
        public void GetAllReturnsResultsList()
        {
            // Arrange
            var project1 = new Mock<Project>().Object;
            var project2 = new Mock<Project>().Object;

            var list = new List<Project> { project1, project2 };

            this.projectRepositoryMock.Setup(pr => pr.List()).Returns(list);

            // Act
            var result = this.projectService.GetAll();

            // Assert
            Assert.Equal(list, result);
        }

        [Fact(DisplayName = "CreateMany with entries returns created list")]
        public void CreateManyWithEntriesReturnsCreatedList()
        {
            var project1Mock = new Mock<Project>();
            var project2Mock = new Mock<Project>();
            var project1 = project1Mock.Object;
            var project2 = project2Mock.Object;

            var list = new HashSet<Project> { project1, project2 };

            this.projectRepositoryMock.Setup(pr => pr.Add(project1));
            this.projectRepositoryMock.Setup(pr => pr.Add(project2));

            var result = this.projectService.CreateMany(list);

            this.unitOfWorkMock.Verify(uow => uow.BeginTransaction());
            this.unitOfWorkMock.Verify(uow => uow.CommitTransaction());
            project1Mock.Verify(p => p.UpdateTimeStamps(), Times.Once);
            project2Mock.Verify(p => p.UpdateTimeStamps(), Times.Once);
            result.Should().Contain(project1);
            result.Should().Contain(project2);
        }
    }
}