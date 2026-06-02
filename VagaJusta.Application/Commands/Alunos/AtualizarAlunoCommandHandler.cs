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
    public record AtualizarAlunoCommandHandler(IAlunoRepository alunoRepository) : IRequestHandler<AtualizarAlunoCommand, AlunoResponse>
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        public async Task<AlunoResponse> Handle(AtualizarAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if (aluno is null)
                throw new NotFoundException("Aluno não encontrado.");

            aluno.Atualizar(request.Nome, request.DataNascimento);
            await _alunoRepository.AtualizarAlunoAsync(cancellationToken);

            return aluno.ToResponse();
        }
    }
}
