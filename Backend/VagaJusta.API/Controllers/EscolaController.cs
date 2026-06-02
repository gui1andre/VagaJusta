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

        [HttpGet("{escolaId:guid}")]
        public async Task<IActionResult> ObterPorId(Guid escolaId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterEscolaPorIdQuery(escolaId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarEscolaCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(ObterPorId), new { escolaId = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarEscolaCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{escolaId:guid}")]
        public async Task<IActionResult> Remover(Guid escolaId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletarEscolaCommand(escolaId), cancellationToken);
            return NoContent();
        }

    }
}
