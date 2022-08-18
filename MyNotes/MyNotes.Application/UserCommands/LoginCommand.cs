using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MyNotes.Application.UserCommands;

public class LoginCommand : IRequest<string>
{
    [Required]
    public string UserName;
    [Required]
    public string Password;
}