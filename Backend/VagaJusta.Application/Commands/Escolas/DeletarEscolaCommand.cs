using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.Commands.Escolas
{
    public record DeletarEscolaCommand(Guid Id) : IRequest
    {
    }
}
