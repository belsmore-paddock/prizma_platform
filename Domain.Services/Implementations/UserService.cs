namespace Prizma.Domain.Services.Implementations
{
    using System;
    using System.Collections.Generic;

    using Prizma.Domain.Models;
    using Prizma.Domain.Repositories;
    using Prizma.Domain.Services.Exceptions;
    using Prizma.Domain.Services.Interfaces;

    /// <summary>
    /// The user service.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public User Create(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.Id;

            var userRepository = this.unitOfWork.FindRepository<IUserRepository>();

            var persistedEntity = this.GetByIdOrDefault(id);
            if (persistedEntity != null)
            {
                throw new EntityAlreadyExistsException(persistedEntity.GetType(), id);
            }

            entity.UpdateTimeStamps();

            this.unitOfWork.BeginTransaction();
            var user = userRepository.Add(entity);
            this.unitOfWork.CommitTransaction();

            return user;
        }

        /// <inheritdoc />
        public User Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var id = user.Id;

            var userRepository = this.unitOfWork.FindRepository<IUserRepository>();

            var persistedUser = this.GetById(id);

            persistedUser.UpdateFrom(user);
            persistedUser.UpdateTimeStamps();
             
            this.unitOfWork.BeginTransaction();
            persistedUser = userRepository.Update(persistedUser);
            this.unitOfWork.CommitTransaction();

            return persistedUser;
        }

        /// <inheritdoc />
        public IList<User> GetAll()
        {
            var userRepository = this.unitOfWork.FindRepository<IUserRepository>();
            var users = userRepository.List();

            return users;
        }

        /// <inheritdoc />
        public User GetById(Guid id)
        {
            var user = this.GetByIdOrDefault(id);

            if (user == null)
            {
                throw new EntityDoesNotExistException(typeof(User), id);
            }

            return user;
        }

        /// <inheritdoc />
        public User GetByIdOrDefault(Guid id)
        {
            var userRepository = this.unitOfWork.FindRepository<IUserRepository>();
            return userRepository.FindById(id);
        }

        /// <inheritdoc />
        public bool Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userRepository = this.unitOfWork.FindRepository<IUserRepository>();

            this.unitOfWork.BeginTransaction();
            var persistedUser = userRepository.FindById(user.Id);
            var isDeleted = userRepository.Delete(persistedUser);
            this.unitOfWork.CommitTransaction();

            return isDeleted;
        }

        /// <inheritdoc />
        public IList<User> CreateMany(ISet<User> userSet)
        {
            var users = new List<User>();

            var userRepository = this.unitOfWork.FindRepository<IUserRepository>();

            this.unitOfWork.BeginTransaction();

            foreach (var user in userSet)
            {
                user.UpdateTimeStamps();
                userRepository.Add(user);
                users.Add(user);
            }

            this.unitOfWork.CommitTransaction();

            return users;
        }
    }
}