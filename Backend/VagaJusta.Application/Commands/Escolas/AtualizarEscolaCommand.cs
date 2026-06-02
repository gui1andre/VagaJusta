using MediatR;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Commands.Escolas
{
    public record AtualizarEscolaCommand(Guid Id, string Nome, string Endereco) : IRequest<EscolaResponse>;
}
