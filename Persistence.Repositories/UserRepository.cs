using Microsoft.EntityFrameworkCore;
using Prizma.Domain.Models;
using Prizma.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prizma.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
