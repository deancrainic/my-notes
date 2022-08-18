using MediatR;
using Microsoft.AspNetCore.Identity;
using MyNotes.Application.UserCommands;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.UserCommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUnitOfWork _uow;
    private readonly IAuthManager _authManager;
    private readonly UserManager<User> _userManager;

    public UpdateUserCommandHandler(IUnitOfWork uow, IAuthManager authManager, UserManager<User> userManager)
    {
        _uow = uow;
        _authManager = authManager;
        _userManager = userManager;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new Exception("User was not found");
        
        if (!_authManager.CheckEmail(request.Email))
            throw new Exception("Email is not valid");
        
        user.UserName = request.UserName;
        user.Email = request.Email;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return user;
        
        throw new Exception(_authManager.GetErrors(result));
    }
}