using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagaJusta.Application.Commands.Matriculas;
using VagaJusta.Application.Queries.Matriculas;

namespace VagaJusta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatriculaController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet("{turmaId:guid}/fila")]
        public async Task<IActionResult> ObterDeFilaEspera(Guid turmaId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterFilaDeEsperaQuery(turmaId), cancellationToken);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Solicitar(SolicitarMatriculaCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(ObterDeFilaEspera), new { turmaId = request.TurmaId }, result);
        }
    }
}
