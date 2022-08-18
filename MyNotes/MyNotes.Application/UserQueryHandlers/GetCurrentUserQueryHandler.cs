using MediatR;
using MyNotes.Application.UserQueries;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.UserQueryHandlers;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, User>
{
    private readonly IUnitOfWork _uow;
    private readonly IAuthManager _authManager;

    public GetCurrentUserQueryHandler(IUnitOfWork uow, IAuthManager authManager)
    {
        _uow = uow;
        _authManager = authManager;
    }

    public async Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);

        var user = await _uow.UserQueryRepository.GetUserByIdAsync(userId);
        
        if (user == null)
            throw new Exception("User was not found");
        
        return user;
    }
}