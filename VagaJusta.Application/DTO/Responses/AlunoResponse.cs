using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.DTO.Responses
{
    public record AlunoResponse(Guid Id, string Nome, string CPF, DateTime DataNascimento, int Idade)
    {
    }
}
