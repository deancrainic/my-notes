using MediatR;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.NoteQuries;

public class GetNoteByIdQuery : IRequest<Note>
{
    public string Token { get; set; }
    public Guid Id { get; set; }
}