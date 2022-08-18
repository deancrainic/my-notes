using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Domain.Repositories
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}
