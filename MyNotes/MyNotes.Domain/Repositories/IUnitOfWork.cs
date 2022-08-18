using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUserCommandRepository UserCommandRepository { get; }
        IUserQueryRepository UserQueryRepository { get; }
        INoteCommandRepository NoteCommandRepository { get; }
        INoteQueryRepository NoteQueryRepository { get; }

        Task SaveAsync();
    }
}
