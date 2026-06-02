using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;

namespace VagaJusta.Application.Commands.Login
{
    public record LoginCommand(string Email, string Senha) : IRequest<TokenResponse>
    {
    }
}
