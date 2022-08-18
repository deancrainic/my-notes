using MediatR;
using MyNotes.Application.NoteCommands;
using MyNotes.Domain.Entities;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;

namespace MyNotes.Application.NoteCommandHandlers
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Note>
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuthManager _authManager;

        public CreateNoteCommandHandler(IUnitOfWork uow, IAuthManager authManager)
        {
            _uow = uow;
            _authManager = authManager;
        }

        public async Task<Note> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var createdNote = new Note
            {
                Title = request.Title,
                Text = request.Text,
                UserId = _authManager.GetUserIdFromToken(request.Token)
            };

            var response = await _uow.NoteCommandRepository.AddAsync(createdNote);
            await _uow.SaveAsync();

            return response;
        }
    }
}
