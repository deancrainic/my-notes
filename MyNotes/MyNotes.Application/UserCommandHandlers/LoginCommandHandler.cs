using MediatR;
using MyNotes.Application.UserCommands;
using MyNotes.Domain.DTOs;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.UserCommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IAuthManager _authManager;

    public LoginCommandHandler(IAuthManager authManager)
    {
        _authManager = authManager;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userDto = new UserLoginDto
        {
            UserName = request.UserName,
            Password = request.Password
        };

        if (!await _authManager.ValidateUser(userDto))
            throw new Exception("Invalid credentials");

        return _authManager.CreateToken();
    }
}