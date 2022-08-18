using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Application.DTOs.NoteDTOs;
using MyNotes.Application.NoteCommands;

namespace MyNotes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromHeader] string authorization, [FromBody] CreateNoteDto newNote)
        {
            // string authHeader = HttpContext.Request.Headers["Authorization"];
            string token = authorization.Split(" ")[1];
            
            var command = new CreateNoteCommand
            {
                Token = token,
                Title = newNote.Title,
                Text = newNote.Text
            };
 
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
