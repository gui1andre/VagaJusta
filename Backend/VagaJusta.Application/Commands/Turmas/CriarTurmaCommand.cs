using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Commands.Turmas
{
    public record CriarTurmaCommand(
        Guid EscolaId,
        string Serie,
        string CategoriaSerie,
        int CapacidadeMaxima,
        int IdadeMinima,
        int IdadeMaxima) : IRequest<TurmaResponse>
    {
    }
}
