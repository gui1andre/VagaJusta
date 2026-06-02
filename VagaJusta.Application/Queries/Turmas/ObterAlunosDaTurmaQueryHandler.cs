using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Queries.Turmas
{
    public class ObterAlunosDaTurmaQueryHandler(IMatriculaRepository matriculaRepository) : IRequestHandler<ObterAlunosDaTurmaQuery, IEnumerable<AlunoResponse>>
    {
        private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;

        public async Task<IEnumerable<AlunoResponse>> Handle(ObterAlunosDaTurmaQuery request, CancellationToken cancellationToken)
        {
            var matriculas = await _matriculaRepository.ListarMatriculasPorTurmaAsync(request.TurmaId, cancellationToken);
            return matriculas.Select(m => new AlunoResponse(
                m.AlunoId,
                m.Aluno!.Nome,
                m.Aluno.CPF.Numero,
                m.Aluno.DataNascimento,
                m.Aluno.Idade
            ));
        }
    }
}
