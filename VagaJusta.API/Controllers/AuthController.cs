using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VagaJusta.Application.Commands.Login;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var command = new LoginCommand(request.Email, request.Senha);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
