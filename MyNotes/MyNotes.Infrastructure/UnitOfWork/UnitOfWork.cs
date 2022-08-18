using MyNotes.Domain.Repositories;
using MyNotes.Infrastructure.Data;
using MyNotes.Infrastructure.UnitOfWork.Repositories;

namespace MyNotes.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyNotesDbContext _context;

        public UnitOfWork(MyNotesDbContext context)
        {
            _context = context;
        }

        public IUserCommandRepository UserCommandRepository => new UserCommandRepository(_context);

        public IUserQueryRepository UserQueryRepository => new UserQueryRepository(_context);

        public INoteCommandRepository NoteCommandRepository => new NoteCommandRepository(_context);

        public INoteQueryRepository NoteQueryRepository => new NoteQueryRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
