using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Queries.Escola
{
    public class ListarEscolaQueryHandler(IEscolaRepository respository) : IRequestHandler<ListarEscolaQuery, IEnumerable<EscolaResponse>>
    {
        private readonly IEscolaRepository _repository = respository;

        public async Task<IEnumerable<EscolaResponse>> Handle(ListarEscolaQuery request, CancellationToken cancellationToken)
        {
            var escolas = await _repository.ObterAsync(request.Pagina, request.TamanhoPagina, cancellationToken);

            return escolas.Select(x => x.ToResponse());
        }
    }
}
