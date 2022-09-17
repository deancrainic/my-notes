using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNotes.Application.DTOs.UserDTOs;
using MyNotes.Application.UserCommands;
using MyNotes.Application.UserQueries;

namespace MyNotes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {      
                var command = new CreateUserCommand
                {
                    UserName = newUser.UserName,
                    Password = newUser.Password,
                    Email = newUser.Email
                };
                
                var result = await _mediator.Send(command);
                var mappedResult = _mapper.Map<GetUserDto>(result);
                
                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto userCredentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = new LoginCommand
                {
                    UserName = userCredentials.UserName,
                    Password = userCredentials.Password
                };
 
                var result = await _mediator.Send(command);
                
                return Accepted(new {Token = result});
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser([FromHeader] string authorization)
        {
            var token = authorization.Split(" ")[1];
            
            try
            {
                var query = new GetCurrentUserQuery
                {
                    Token = token
                };
                
                var result = await _mediator.Send(query);
                var mappedResult = _mapper.Map<GetUserDto>(result);
                
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromHeader] string authorization, [FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var token = authorization.Split(" ")[1];

            try
            {
                var command = new UpdateUserCommand
                {
                    Token = token,
                    Email = userDto.Email,
                    UserName = userDto.UserName
                };

                var result = await _mediator.Send(command);
                var mappedResult = _mapper.Map<GetUserDto>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPatch]
        [Route("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromHeader] string authorization,
            [FromBody] ChangeUserPasswordDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = authorization.Split(" ")[1];

            try
            {
                var command = new ChangeUserPasswordCommand
                {
                    Token = token,
                    Password = userDto.Password
                };

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromHeader] string authorization)
        {
            var token = authorization.Split(" ")[1];

            try
            {
                var command = new DeleteUserCommand
                {
                    Token = token
                };

                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
