using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Application.DTOs.NoteDTOs;
using MyNotes.Application.NoteCommands;
using MyNotes.Application.NoteQuries;

namespace MyNotes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public NotesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromHeader] string authorization, [FromBody] CreateNoteDto newNote)
        {
            string token = authorization.Split(" ")[1];
            
            var command = new CreateNoteCommand
            {
                Token = token,
                Title = newNote.Title,
                Text = newNote.Text
            };
 
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<GetNoteDto>(result);

            return Ok(mappedResult);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllNotesFromCurrentUser([FromHeader] string authorization)
        {
            var token = authorization.Split(" ")[1];

            try
            {
                var query = new GetAllNotesFromCurrentUserQuery
                {
                    Token = token
                };

                var result = await _mediator.Send(query);
                var mappedResult = _mapper.Map<IEnumerable<GetNoteDto>>(result);
                
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        
        [Authorize]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetNoteById([FromHeader] string authorization, [FromRoute] Guid id)
        {
            var token = authorization.Split(" ")[1];

            try
            {
                var query = new GetNoteByIdQuery
                {
                    Token = token,
                    Id = id
                };

                var result = await _mediator.Send(query);
                var mappedResult = _mapper.Map<GetNoteDto>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateNoteById([FromHeader] string authorization, [FromRoute] Guid id, 
            [FromBody] UpdateNoteDto noteDto)
        {
            var token = authorization.Split(" ")[1];

            try
            {
                var command = new UpdateNoteCommand()
                {
                    Token = token,
                    Id = id,
                    Title = noteDto.Title,
                    Text = noteDto.Text
                };

                var result = await _mediator.Send(command);
                var mappedResult = _mapper.Map<GetNoteDto>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteNoteById([FromHeader] string authorization, [FromRoute] Guid id)
        {
            var token = authorization.Split(" ")[1];

            try
            {
                var command = new DeleteNoteCommand
                {
                    Token = token,
                    Id = id
                };

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
