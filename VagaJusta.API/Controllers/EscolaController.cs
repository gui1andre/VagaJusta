using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagaJusta.Application.Commands.Escolas;
using VagaJusta.Application.Queries.Escolas;

namespace VagaJusta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EscolaController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Obter([FromQuery] ListarEscolaQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterEscolaPorIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarEscolaCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
        }

    }
}
