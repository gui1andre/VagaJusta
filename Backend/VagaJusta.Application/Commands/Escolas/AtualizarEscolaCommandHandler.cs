using MediatR;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Exceptions;
using VagaJusta.Application.Mapping;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Application.Commands.Escolas
{
    public class AtualizarEscolaCommandHandler(IEscolaRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<AtualizarEscolaCommand, EscolaResponse>
    {
        private readonly IEscolaRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<EscolaResponse> Handle(AtualizarEscolaCommand request, CancellationToken cancellationToken)
        {
            var escola = await _repository.ObterPorIdAsync(request.Id, cancellationToken);

            if (escola is null)
                throw new NotFoundException("Escola não encontrada.");

            escola.Atualizar(request.Nome, request.Endereco);
            await _unitOfWork.CommitAsync(cancellationToken);

            return escola.ToResponse();
        }
    }
}
