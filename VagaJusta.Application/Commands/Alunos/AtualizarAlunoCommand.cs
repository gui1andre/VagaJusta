using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Commands.Alunos
{
    public record AtualizarAlunoCommand(Guid Id, string Nome, DateTime DataNascimento): IRequest<AlunoResponse>
    {
    }
}
