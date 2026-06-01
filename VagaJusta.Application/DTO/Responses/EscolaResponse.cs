using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.DTO.Responses
{
    public record EscolaResponse(Guid Id, string Nome, string Endereco, IEnumerable<TurmaResponse> Turmas)
}
