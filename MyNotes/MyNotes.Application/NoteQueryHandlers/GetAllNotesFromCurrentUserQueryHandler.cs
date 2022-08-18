using MediatR;
using MyNotes.Application.NoteQuries;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.NoteQueryHandlers;

public class GetAllNotesFromCurrentUserQueryHandler : IRequestHandler<GetAllNotesFromCurrentUserQuery, IEnumerable<Note>>
{
    private readonly IUnitOfWork _uow;
    private readonly IAuthManager _authManager;

    public GetAllNotesFromCurrentUserQueryHandler(IUnitOfWork uow, IAuthManager authManager)
    {
        _uow = uow;
        _authManager = authManager;
    }

    public async Task<IEnumerable<Note>> Handle(GetAllNotesFromCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);
        var user = await _uow.UserQueryRepository.GetUserByIdAsync(userId);

        if (user == null)
            throw new Exception("User was not found");
        
        var notes = await _uow.NoteQueryRepository.GetAllNotesByUserAsync(user);

        return notes;
    }
}