using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Responses;
using VagaJusta.Application.Interfaces;

namespace VagaJusta.Application.Commands.Login
{
    public class LoginCommandHandler(IIdentityService identityService, ITokenService tokenService) : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _identityService.ObterPorEmailAsync(request.Email);

            if(usuario is null || !await _identityService.ValidarSenhaAsync(usuario.Id, request.Senha))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            if (!usuario.Ativo)
                throw new UnauthorizedAccessException("Usuário inativo.");

            var token = _tokenService.GerarToken(usuario.Id, usuario.Email);
            return new TokenResponse(token);
        }
    }
}
