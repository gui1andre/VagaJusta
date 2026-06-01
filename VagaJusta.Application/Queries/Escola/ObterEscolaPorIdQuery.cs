using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Queries.Escola
{
    public record ObterEscolaPorIdQuery(Guid Id) : IRequest<EscolaResponse>
    {
    }
}
