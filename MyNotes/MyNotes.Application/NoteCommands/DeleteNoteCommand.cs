using MediatR;

namespace MyNotes.Application.NoteCommands;

public class DeleteNoteCommand : IRequest
{
    public string Token { get; set; }
    public Guid Id { get; set; }
}