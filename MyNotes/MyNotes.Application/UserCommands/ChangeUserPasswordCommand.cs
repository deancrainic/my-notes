using MediatR;

namespace MyNotes.Application.UserCommands;

public class ChangeUserPasswordCommand : IRequest
{
    public string Token { get; set; }
    public string Password { get; set; }
}