using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.DTO.Responses
{
    public record MatriculaResponse(Guid Id, Guid AlunoId, string NomeAluno, string CPFAluno, string Turma, string Escola, string Status, DateTime Solicitacao )
    {
    }
}
