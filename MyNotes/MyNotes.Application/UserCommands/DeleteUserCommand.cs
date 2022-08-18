using MediatR;

namespace MyNotes.Application.UserCommands;

public class DeleteUserCommand : IRequest
{
    public string Token { get; set; }
}