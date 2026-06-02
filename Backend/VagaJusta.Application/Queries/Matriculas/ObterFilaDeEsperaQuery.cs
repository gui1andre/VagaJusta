using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Queries.Matriculas
{
    public record ObterFilaDeEsperaQuery(Guid TurmaId) : IRequest<IEnumerable<FilaEsperaResponse>>
    {
    }
}
