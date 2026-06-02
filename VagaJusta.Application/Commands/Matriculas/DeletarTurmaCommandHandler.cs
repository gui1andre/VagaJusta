using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Exceptions;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Commands.Matriculas
{
    public class DeletarTurmaCommandHandler(ITurmaRepository turmaRepository, IMatriculaRepository matriculaRepository) : IRequestHandler<DeletarTurmaCommand>
    {
        private readonly ITurmaRepository _turmaRepository = turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;

        public async Task Handle(DeletarTurmaCommand request, CancellationToken cancellationToken)
        {
            var turma = await _turmaRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if(turma is null)
                throw new DomainException("Turma não encontrada.");

            var temMatriculas = await _matriculaRepository.TurmaTemMatriculaAtivaAsync(request.Id, cancellationToken);

            if(temMatriculas)
                throw new DomainException("Não é possível excluir uma turma que possui matrículas ativas.");

            await _turmaRepository.RemoverAsync(turma, cancellationToken);
        }
    }
}
