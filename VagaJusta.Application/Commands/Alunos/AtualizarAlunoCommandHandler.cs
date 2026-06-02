using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Exceptions;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Commands.Alunos
{
    public record AtualizarAlunoCommandHandler(IAlunoRepository alunoRepository, IUnitOfWork unitOfWork) : IRequestHandler<AtualizarAlunoCommand, AlunoResponse>
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<AlunoResponse> Handle(AtualizarAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if (aluno is null)
                throw new NotFoundException("Aluno não encontrado.");

            aluno.Atualizar(request.Nome, request.DataNascimento);
            await _unitOfWork.CommitAsync(cancellationToken);

            return aluno.ToResponse();
        }
    }
}
