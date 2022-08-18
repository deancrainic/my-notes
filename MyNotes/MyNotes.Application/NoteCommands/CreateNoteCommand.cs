using MediatR;
using MyNotes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Application.NoteCommands
{
    public class CreateNoteCommand : IRequest<Note>
    {
        public string Token { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
