namespace Prizma.Domain.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// The User Repository <see langword="interface"/> handles interaction with the persistence layer.
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}