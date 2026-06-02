using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.Commands.Matriculas
{
    public record DeletarTurmaCommand(Guid Id) : IRequest;
}
