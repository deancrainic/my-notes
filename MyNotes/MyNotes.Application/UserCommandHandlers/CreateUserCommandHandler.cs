using System.Net.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyNotes.Application.UserCommands;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.UserCommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthManager _authManager;
        
        public CreateUserCommandHandler(UserManager<User> userManager, IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email
            };

            if (!_authManager.CheckEmail(user.Email))
                throw new Exception("Email is not valid");

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return user;
            
            throw new Exception(_authManager.GetErrors(result));
        }
    }
}
