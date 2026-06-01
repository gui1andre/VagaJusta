using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Queries.Escolas
{
    public record ListarEscolaQuery(int Pagina = 1, int TamanhoPagina = 10) : IRequest<IEnumerable<EscolaResponse>>;
}
