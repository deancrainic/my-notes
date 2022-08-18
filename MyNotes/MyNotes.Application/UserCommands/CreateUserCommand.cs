using MediatR;
using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Application.UserCommands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
