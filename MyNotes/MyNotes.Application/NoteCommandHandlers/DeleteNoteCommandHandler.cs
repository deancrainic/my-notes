using MediatR;
using MyNotes.Application.NoteCommands;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.NoteCommandHandlers;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly IUnitOfWork _uow;
    private IAuthManager _authManager;

    public DeleteNoteCommandHandler(IUnitOfWork uow, IAuthManager authManager)
    {
        _uow = uow;
        _authManager = authManager;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);

        var note = await _uow.NoteQueryRepository.GetNoteByIdAsync(request.Id);

        if (note == null)
            throw new Exception("Note doesn't exist");

        if (!note.UserId.Equals(userId))
            throw new Exception("Note isn't yours");

        await _uow.NoteCommandRepository.DeleteAsync(note);
        await _uow.SaveAsync();

        return new Unit();
    }
}