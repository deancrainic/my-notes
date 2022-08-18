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
    public class NoteCommandRepository : CommandRepository<Note>, INoteCommandRepository
    {
        public NoteCommandRepository(MyNotesDbContext context) : base(context)
        {
        }
    }
}
