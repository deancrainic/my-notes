using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Infrastructure.UnitOfWork.Repositories
{
    public class UserQueryRepository : IUserQueryRepository
    {
        MyNotesDbContext _context;
        DbSet<User> _dbSet;

        public UserQueryRepository(MyNotesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id.ToString());
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        }
    }
}
