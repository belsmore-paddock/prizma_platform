namespace Prizma.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Prizma.Domain.Models;
    using Prizma.Domain.Repositories;

    /// <inheritdoc />
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity, Guid> where TEntity : DomainBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected RepositoryBase(DbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        protected DbContext Context { get; }

        /// <inheritdoc />
        public virtual TEntity Add(TEntity entity) => this.Context.Add(entity).Entity;

        /// <inheritdoc />
        public virtual TEntity Update(TEntity entity) => this.Context.Update(entity).Entity;

        /// <inheritdoc />
        public virtual bool Delete(TEntity entity)
        {
            this.Context.Remove(entity);
            return true;
        }

        /// <inheritdoc />
        public virtual IList<TEntity> List() => this.Context.Set<TEntity>().ToList();

        /// <inheritdoc />
        public virtual TEntity FindById(Guid id) => this.Context.Find<TEntity>(id);

        /// <inheritdoc />
        public virtual async Task<TEntity> FindByIdAsync(Guid id) => await this.Context.Set<TEntity>().FindAsync(id);

        /// <inheritdoc />
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityTask = await this.Context.Set<TEntity>().AddAsync(entity);
            return entityTask.Entity;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => this.Context.Set<TEntity>().Update(entity).Entity);
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            return await Task.Run(() => this.Delete(entity));
        }

        /// <inheritdoc />
        public virtual async Task<IList<TEntity>> ListAsync()
        {
            return await this.Context.Set<TEntity>().ToListAsync();
        }
    }
}