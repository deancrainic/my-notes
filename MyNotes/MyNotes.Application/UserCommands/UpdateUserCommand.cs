using MediatR;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.UserCommands;

public class UpdateUserCommand : IRequest<User>
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}