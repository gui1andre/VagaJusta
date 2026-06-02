using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.Exceptions;
using VagaJusta.Domain.Exceptions;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Commands.Escolas
{
    public record DeletarEscolaCommandHandler(IEscolaRepository escolaRepository, IMatriculaRepository matriculaRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeletarEscolaCommand>
    {
        private readonly IEscolaRepository _escolaRepository = escolaRepository;
        private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(DeletarEscolaCommand request, CancellationToken cancellationToken)
        {
            var escola = await _escolaRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if (escola is null)
                throw new NotFoundException("Escola não encontrada.");

            var temMatricula = await _matriculaRepository.EscolaTemMatriculaAtivaAsync(request.Id, cancellationToken);

            if (temMatricula)
                throw new DomainException("Não é possível deletar escolas com matriculas ativas.");

            _escolaRepository.RemoverAsync(escola, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
