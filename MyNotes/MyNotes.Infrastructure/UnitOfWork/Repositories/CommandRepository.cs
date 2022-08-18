using Microsoft.EntityFrameworkCore;
using MyNotes.Domain.Repositories;
using MyNotes.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Infrastructure.UnitOfWork.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly MyNotesDbContext _context;
        private readonly DbSet<T> _dbSet;

        public CommandRepository(MyNotesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
