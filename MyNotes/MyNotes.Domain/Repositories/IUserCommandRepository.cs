using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Domain.Repositories
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {
    }
}
