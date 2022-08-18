using MediatR;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.NoteCommands;

public class UpdateNoteCommand : IRequest<Note>
{
    public string Token { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}