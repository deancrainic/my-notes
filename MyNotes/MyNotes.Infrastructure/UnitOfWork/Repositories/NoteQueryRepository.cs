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
    public class NoteQueryRepository : INoteQueryRepository
    {
        MyNotesDbContext _context;
        DbSet<Note> _dbSet;

        public NoteQueryRepository(MyNotesDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Note>();
        }
        public async Task<IEnumerable<Note>> GetAllNotesByUserAsync(User user)
        {
            return await _dbSet.Where(n => n.UserId.Equals(user.Id)).ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
