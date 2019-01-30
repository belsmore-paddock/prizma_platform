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
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.Id;

            var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

            var project = this.GetByIdOrDefault(id);
            if (project != null)
            {
                throw new EntityAlreadyExistsException(project.GetType(), id);
            }

            entity.UpdateTimeStamps();

            this.unitOfWork.BeginTransaction();
            project = projectRepository.Add(entity);
            this.unitOfWork.CommitTransaction();

            return project;
        }

        public Project GetByIdOrDefault(Guid id)
        {
            var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();
            return projectRepository.FindById(id);
        }

        /// <inheritdoc />
        public Project Update(Project entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.Id;
            var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

            var project = this.GetById(id);

            project.UpdateFrom(entity);
            project.UpdateTimeStamps();

            this.unitOfWork.BeginTransaction();
            project = projectRepository.Update(project);
            this.unitOfWork.CommitTransaction();

            return project;
        }

        /// <inheritdoc />
        public IList<Project> GetAll()
        {
            var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();
            var projects = projectRepository.List();

            return projects;
        }

        /// <inheritdoc />
        public Project GetById(Guid id)
        {
            var project = this.GetByIdOrDefault(id);

            if (project == null)
            {
                throw new EntityDoesNotExistException(typeof(Project), id);
            }

            return project;
        }

        /// <inheritdoc />
        public bool Delete(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

            this.unitOfWork.BeginTransaction();
            var entity = projectRepository.FindById(project.Id);
            var isDeleted = projectRepository.Delete(entity);
            this.unitOfWork.CommitTransaction();

            return isDeleted;
        }

        /// <inheritdoc />
        public IList<Project> CreateMany(ISet<Project> projectSet)
        {
            var projects = new List<Project>();

            var projectRepository = this.unitOfWork.FindRepository<IProjectRepository>();

            this.unitOfWork.BeginTransaction();

            foreach (var project in projectSet)
            {
                project.UpdateTimeStamps();
                projectRepository.Add(project);
                projects.Add(project);
            }

            this.unitOfWork.CommitTransaction();

            return projects;
        }
    }
}