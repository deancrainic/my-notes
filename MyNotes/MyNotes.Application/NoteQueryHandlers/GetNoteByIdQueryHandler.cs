using MediatR;
using MyNotes.Application.NoteQuries;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.NoteQueryHandlers;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Note>
{
    private readonly IUnitOfWork _uow;
    private readonly IAuthManager _authManager;

    public GetNoteByIdQueryHandler(IUnitOfWork uow, IAuthManager authManager)
    {
        _uow = uow;
        _authManager = authManager;
    }

    public async Task<Note> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);
        var note = await _uow.NoteQueryRepository.GetNoteByIdAsync(request.Id);

        if (note == null)
            throw new Exception("Note doesn't exist");
        
        if (note.UserId.Equals(userId))
            return note;

        throw new Exception("Note isn't yours");
    }
}