using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Queries.Escolas
{
    public class ObterEscolaPorIdHandler(IEscolaRepository repository) : IRequestHandler<ObterEscolaPorIdQuery, EscolaResponse?>
    {
        private readonly IEscolaRepository _repository = repository;

        public async Task<EscolaResponse?> Handle(ObterEscolaPorIdQuery request, CancellationToken cancellationToken)
        {
            var escola = await _repository.ObterPorIdAsync(request.Id, cancellationToken);
            return escola?.ToResponse();
        }
    }
}
