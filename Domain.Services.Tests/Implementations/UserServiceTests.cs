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
    public class UserServiceTests
    {
        /// <summary>
        /// The user service SUT.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The unit of work mock.
        /// </summary>
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        /// <summary>
        /// The user repository mock.
        /// </summary>
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectServiceTest"/> class.
        /// </summary>
        public UserServiceTests()
        {
            this.unitOfWorkMock.Setup(uow => uow.FindRepository<IUserRepository>())
                .Returns(this.userRepositoryMock.Object);
            this.userService = new UserService(this.unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Create method with new entity returns resulting created model.")]
        public void CreateWithNewEntityReturnsCreatedModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sourceUserMock = new Mock<User>();
            sourceUserMock.Setup(p => p.Id).Returns(id);
            var sourceProject = sourceUserMock.Object;

            User nullProject = null;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(nullProject);
            this.userRepositoryMock.Setup(pr => pr.Add(sourceProject)).Returns(sourceProject);

            // Act
            var result = this.userService.Create(sourceProject);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sourceProject, result);
            sourceUserMock.Verify(p => p.UpdateTimeStamps(), Times.Once);
        }

        [Fact(DisplayName = "Create method when entity already exists throws an exception.")]
        public void CreateWhenEntityAlreadyExistThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var createUserMock = new Mock<User>();
            createUserMock.Setup(p => p.Id).Returns(id);
            var createUser = createUserMock.Object;

            var updateUser = new UserBuilder()
                .WithId(id)
                .WithName("Joe Smith")
                .WithUserName("jsmith")
                .WithPasswordHash("3423423")
                .WithEmail("test@test.com").Build();

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(updateUser);

            // Act
            var exception = Assert.Throws<EntityAlreadyExistsException>(() => this.userService.Create(createUser));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.User with Id {id} already exists.", exception.Message);
        }

        [Fact(DisplayName = "Create method when entity parameter is null throws an exception.")]
        public void CreateWhenEntityParameterIsNullThrowsException()
        {
            // Arrange
            User user = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => this.userService.Create(user));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Value cannot be null.{Environment.NewLine}Parameter name: entity", exception.Message);
        }

        [Fact(DisplayName = "Update method with existing entity returns resulting updated model.")]
        public void UpdateWithExistingEntityReturnsUpdatedModel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sourceUserMock = new Mock<User>();
            sourceUserMock.Setup(p => p.Id).Returns(id);
            var sourceUser = sourceUserMock.Object;

            var updateUserMock = new Mock<User>();
            var updateUser = updateUserMock.Object;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(updateUser);
            this.userRepositoryMock.Setup(pr => pr.Update(updateUser)).Returns(updateUser);

            // Act
            var result = this.userService.Update(sourceUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateUser, result);

            updateUserMock.Verify(p => p.UpdateFrom(sourceUser), Times.Once);
            updateUserMock.Verify(p => p.UpdateTimeStamps(), Times.Once);
        }

        [Fact(DisplayName = "Update method when entity does not already exists throws an exception.")]
        public void UpdateWhenEntityDoesNotExistThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userMock = new Mock<User>();
            userMock.Setup(p => p.Id).Returns(id);
            var user = userMock.Object;

            User nullUser = null;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(nullUser);

            // Act
            var exception = Assert.Throws<EntityDoesNotExistException>(() => this.userService.Update(user));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.User with Id {id} does not exist.", exception.Message);
        }

        [Fact(DisplayName = "Update method when entity parameter is null throws an exception.")]
        public void UpdateWhenEntityParameterIsNullThrowsException()
        {
            // Arrange
            User user = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => this.userService.Update(user));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Value cannot be null.{Environment.NewLine}Parameter name: user", exception.Message);
        }

        [Fact(DisplayName = "Delete method when valid Id is passed returns true.")]
        public void DeleteWhenValidIdIsPassedReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userMock = new Mock<User>();
            userMock.Setup(p => p.Id).Returns(id);
            var user = userMock.Object;

            var existingUserMock = new Mock<User>();
            var existingUser = existingUserMock.Object;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(existingUser);
            this.userRepositoryMock.Setup(pr => pr.Delete(existingUser)).Returns(true);

            // Act
            var result = this.userService.Delete(user);

            // Assert
            Assert.True(result);

            this.unitOfWorkMock.Verify(uow => uow.BeginTransaction(), Times.Once);
            this.unitOfWorkMock.Verify(uow => uow.CommitTransaction());
        }

        [Fact(DisplayName = "Delete method when provided Id cannot be deleted returns false.")]
        public void DeleteWhenProvidedIdCannotBeDeletedReturnsFalse()
        {
            // Arrange
            var userMock = new Mock<User>();
            var user = userMock.Object;

            this.userRepositoryMock.Setup(pr => pr.Delete(user)).Returns(false);

            // Act
            var result = this.userService.Delete(user);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Delete method when provided parameter is null throws exception.")]
        public void DeleteWhenProvidedParameterIsNullThrowsException()
        {
            // Arrange
            User user = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => this.userService.Delete(user));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Value cannot be null.{Environment.NewLine}Parameter name: user", exception.Message);
        }

        [Fact(DisplayName = "Get by id when provided Id returns result.")]
        public void GetByIdWhenProvidedIdReturnsResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userMock = new Mock<User>();
            var user = userMock.Object;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(user);

            // Act
            var result = this.userService.GetById(id);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact(DisplayName = "Get by id when provided Id does not have an entity throws exception.")]
        public void GetByIdWhenProvidedIdDoesntHaveAnEntityThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            User user = null;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(user);

            // Act
            var exception = Assert.Throws<EntityDoesNotExistException>(() => this.userService.GetById(id));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity Type Prizma.Domain.Models.User with Id {id} does not exist.", exception.Message);
        }

        [Fact(DisplayName = "Get by id or default when provided Id returns result.")]
        public void GetByIdOrDefaultWhenProvidedIdReturnsResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userMock = new Mock<User>();
            var user = userMock.Object;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(user);

            // Act
            var result = this.userService.GetByIdOrDefault(id);

            // Assert
            result.Should().Be(user);
        }

        [Fact(DisplayName = "Get by id or default when provided Id does not have an entity throws exception.")]
        public void GetByIdOrDefaultWhenProvidedIdDoesntHaveAnEntityThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            User user = null;

            this.userRepositoryMock.Setup(pr => pr.FindById(id)).Returns(user);

            // Act
            var result = this.userService.GetByIdOrDefault(id);

            // Assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = "GetAll method returns full result list.")]
        public void GetAllReturnsResultsList()
        {
            // Arrange
            var user1 = new Mock<User>().Object;
            var user2 = new Mock<User>().Object;

            var list = new List<User> { user1, user2 };

            this.userRepositoryMock.Setup(pr => pr.List()).Returns(list);

            // Act
            var result = this.userService.GetAll();

            // Assert
            Assert.Equal(list, result);
        }

        [Fact(DisplayName = "CreateMany with entries returns created list")]
        public void CreateManyWithEntriesReturnsCreatedList()
        {
            var user1Mock = new Mock<User>();
            var user2Mock = new Mock<User>();
            var user1 = user1Mock.Object;
            var user2 = user2Mock.Object;

            var list = new HashSet<User> { user1, user2 };

            this.userRepositoryMock.Setup(pr => pr.Add(user1));
            this.userRepositoryMock.Setup(pr => pr.Add(user2));

            var result = this.userService.CreateMany(list);

            this.unitOfWorkMock.Verify(uow => uow.BeginTransaction());
            this.unitOfWorkMock.Verify(uow => uow.CommitTransaction());
            user1Mock.Verify(p => p.UpdateTimeStamps(), Times.Once);
            user2Mock.Verify(p => p.UpdateTimeStamps(), Times.Once);
            result.Should().Contain(user1);
            result.Should().Contain(user2);
        }
    }
}