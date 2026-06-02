using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VagaJusta.Application.Commands.Matriculas;
using VagaJusta.Application.Commands.Turmas;
using VagaJusta.Application.Queries.Turmas;

namespace VagaJusta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TurmaController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Criar(CriarTurmaCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpDelete("{turmaId:guid}")]
        public async Task<IActionResult> Remover(Guid turmaId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeletarTurmaCommand(turmaId), cancellationToken);
            return NoContent();
        }

        [HttpGet("{turmaId:guid}/alunos")]
        public async Task<IActionResult> ObterAlunos(Guid turmaId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterAlunosDaTurmaQuery(turmaId), cancellationToken);
            return Ok(result);
        }

    }
}
