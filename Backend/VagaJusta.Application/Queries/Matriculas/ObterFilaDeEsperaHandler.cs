using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Queries.Matriculas
{
    internal class ObterFilaDeEsperaHandler(IMatriculaRepository repository) : IRequestHandler<ObterFilaDeEsperaQuery, IEnumerable<FilaEsperaResponse>>
    {
        private readonly IMatriculaRepository _repository = repository;

        public async Task<IEnumerable<FilaEsperaResponse>> Handle(ObterFilaDeEsperaQuery request, CancellationToken cancellationToken)
        {
            var fila = await _repository.ListarFilaDeEsperaAsync(request.TurmaId, cancellationToken);

            return fila.OrderByDescending(x => x.DataSolicitacao).Select((x, i) => x.ToFilaEsperaResponse(i + 1));
        }
    }
}
