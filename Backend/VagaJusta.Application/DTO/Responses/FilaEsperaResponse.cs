using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.DTO.Responses
{
    public record FilaEsperaResponse(int Posicao, Guid MatriculaId, AlunoResponse Aluno, DateTime DataSolicitacao)
    {
    }
}
