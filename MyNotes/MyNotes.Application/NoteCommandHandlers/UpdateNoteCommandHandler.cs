using MediatR;
using MyNotes.Application.NoteCommands;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.NoteCommandHandlers;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Note>
{
    private readonly IUnitOfWork _uow;
    private readonly IAuthManager _authManager;

    public UpdateNoteCommandHandler(IUnitOfWork uow, IAuthManager authManager)
    {
        _uow = uow;
        _authManager = authManager;
    }

    public async Task<Note> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _authManager.GetUserIdFromToken(request.Token);

        var note = await _uow.NoteQueryRepository.GetNoteByIdAsync(request.Id);

        if (note == null)
            throw new Exception("Note doesn't exist");

        if (!note.UserId.Equals(userId))
            throw new Exception("Note isn't yours");

        note.Title = request.Title;
        note.Text = request.Text;
        note.LastEdited = DateTime.Now;
        
        await _uow.NoteCommandRepository.UpdateAsync(note);
        await _uow.SaveAsync();
        
        return note;
    }
}