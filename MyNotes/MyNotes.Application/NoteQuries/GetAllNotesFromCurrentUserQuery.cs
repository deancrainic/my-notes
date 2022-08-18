using MediatR;
using MyNotes.Domain.Entities;

namespace MyNotes.Application.NoteQuries;

public class GetAllNotesFromCurrentUserQuery : IRequest<IEnumerable<Note>>
{
    public string Token { get; set; }
}