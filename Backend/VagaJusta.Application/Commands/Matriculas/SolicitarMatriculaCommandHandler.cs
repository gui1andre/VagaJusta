using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Exceptions;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Exceptions;
using VagaJusta.Domain.Interfaces.Repositories;
using VagaJusta.Domain.ValueObjects;

namespace VagaJusta.Application.Commands.Matriculas
{
    public class SolicitarMatriculaCommandHandler(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository, IMatriculaRepository matriculaRepository, IUnitOfWork unitOfWork) : IRequestHandler<SolicitarMatriculaCommand, MatriculaResponse>
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly ITurmaRepository _turmaRepository = turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<MatriculaResponse> Handle(SolicitarMatriculaCommand request, CancellationToken cancellationToken)
        {
            var turma = await _turmaRepository.ObterPorIdAsync(request.TurmaId, cancellationToken);

            if (turma is null)
                throw new NotFoundException("Turma não encontrada.");

            var aluno = await _alunoRepository.ObterPorCPFAsync(new CPF(request.CPFAluno), cancellationToken);

            var alunoJaExiste = aluno is not null;

            if (!alunoJaExiste)
            {
                aluno = Aluno.Criar(request.NomeAluno, request.CPFAluno, request.DataNascimento);
                await _alunoRepository.AdicionarAlunoAsync(aluno, cancellationToken);
            }

            if (alunoJaExiste)
            {
                var jaMatriculado = await _matriculaRepository.AlunoTemMatriculaAtivaAsync(aluno!.Id, cancellationToken);
                if (jaMatriculado)
                    throw new DomainException("Aluno já possui matrícula nessa turma.");
            }

            if (!turma.IdadeAlunoCompativel(aluno!.Idade))
                throw new DomainException("Idade do aluno não é compatível com a turma.");

            var matricula = Matricula.Criar(aluno.Id, turma.Id);

            if (!turma.TurmaLotada)
            {
                turma.IncrementarVagasOcupadas();
                matricula.Aprovar();
            }
            else
                matricula.ColocarEmEspera();

            await _matriculaRepository.AdicionarAsync(matricula, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return matricula.ToResponse();
        }
    }
}
