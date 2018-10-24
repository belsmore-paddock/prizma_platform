namespace Prizma.Domain.Services.Implementations
{
    using System;
    using System.Collections.Generic;

    using Prizma.Domain.Models;
    using Prizma.Domain.Repositories;
    using Prizma.Domain.Services.Exceptions;
    using Prizma.Domain.Services.Interfaces;

    /// <summary>
    /// The project service.
    /// </summary>
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public ProjectService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public Project Create(Project entity)
        {
            var id = entity.Id;
            Project project;

            using (this.unitOfWork)
            {
                var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

                var exists = projectRepository.Exists(id);
                if (exists)
                {
                    throw new EntityAlreadyExistsException(entity.GetType(), id);
                }

                entity.UpdateTimeStamps();

                this.unitOfWork.BeginTransaction();
                project = projectRepository.Add(entity);
                this.unitOfWork.CommitTransaction();
            }

            return project;
        }

        /// <inheritdoc />
        public Project Update(Project entity)
        {
            var id = entity.Id;
            Project project;

            using (this.unitOfWork)
            {
                var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

                var exists = projectRepository.Exists(id);
                if (!exists)
                {
                    throw new EntityDoesNotExistException(entity.GetType(), id);
                }

                entity.UpdateTimeStamps();

                this.unitOfWork.BeginTransaction();
                project = projectRepository.Update(entity);
                this.unitOfWork.CommitTransaction();
            }

            return project;
        }

        /// <inheritdoc />
        public IList<Project> GetAll()
        {
            IList<Project> projects;

            using (this.unitOfWork)
            {
                var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();
                projects = projectRepository.List();
            }

            return projects;
        }

        /// <inheritdoc />
        public Project Get(Guid id)
        {
            Project project;

            using (this.unitOfWork)
            {
                var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();
                project = projectRepository.FindById(id);
            }

            return project;
        }

        /// <inheritdoc />
        public bool Delete(Guid id)
        {
            bool isDeleted;

            using (this.unitOfWork)
            {
                var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

                this.unitOfWork.BeginTransaction();
                isDeleted = projectRepository.Delete(id);
                this.unitOfWork.CommitTransaction();
            }

            return isDeleted;
        }
    }
}