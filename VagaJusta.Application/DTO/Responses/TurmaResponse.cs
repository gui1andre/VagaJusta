using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.DTO.Responses
{
    public record TurmaResponse(
        Guid Id, 
        string Serie, 
        string CategoriaSerie, 
        int CapacidadeMaxima,
        int QuantidadeAlunos,
        int IdadeMinima, 
        int IdadeMaxima, 
        int VagasDisponiveis)
    {
    }
}
