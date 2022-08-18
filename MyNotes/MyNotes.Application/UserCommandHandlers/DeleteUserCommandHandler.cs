using MediatR;
using Microsoft.AspNetCore.Identity;
using MyNotes.Application.UserCommands;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.UserCommandHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthManager _authManager;

    public DeleteUserCommandHandler(UserManager<User> userManager, IAuthManager authManager)
    {
        _userManager = userManager;
        _authManager = authManager;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
            return new Unit();

        throw new Exception(_authManager.GetErrors(result));
    }
}