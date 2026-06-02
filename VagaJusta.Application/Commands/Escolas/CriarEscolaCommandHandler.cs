using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Commands.Escolas
{
    public class CriarEscolaHandler(IEscolaRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<CriarEscolaCommand, EscolaResponse>
    {
        private readonly IEscolaRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<EscolaResponse> Handle(CriarEscolaCommand request, CancellationToken cancellationToken)
        {
            var escola = Escola.Criar(request.Nome, request.Endereco);
            await _repository.AdicionarAsync(escola, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return escola.ToResponse();
        }
    }
}
