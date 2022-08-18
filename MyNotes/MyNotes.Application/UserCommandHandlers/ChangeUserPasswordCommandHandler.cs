using MediatR;
using Microsoft.AspNetCore.Identity;
using MyNotes.Application.UserCommands;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.UserCommandHandlers;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthManager _authManager;
    
    public ChangeUserPasswordCommandHandler(UserManager<User> userManager, IAuthManager authManager)
    {
        _userManager = userManager;
        _authManager = authManager;
    }

    public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, resetToken, request.Password);

        if (result.Succeeded)
            return new Unit();

        throw new Exception(_authManager.GetErrors(result));
    }
}