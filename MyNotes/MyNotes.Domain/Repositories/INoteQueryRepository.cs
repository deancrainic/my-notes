using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Domain.Repositories
{
    public interface INoteQueryRepository : IQueryRepository<Note>
    {
        Task<Note> GetNoteByIdAsync(Guid id);
        Task<IEnumerable<Note>> GetAllNotesByUserAsync(User user);
    }
}
