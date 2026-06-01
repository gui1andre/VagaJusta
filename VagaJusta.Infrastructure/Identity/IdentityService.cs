using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO.Identity;
using VagaJusta.Application.Interfaces;

namespace VagaJusta.Infrastructure.Identity
{
    public class IdentityService(UserManager<UsuarioIdentity> userManager) : IIdentityService
    {
        private readonly UserManager<UsuarioIdentity> _userManager;
        public async Task<IdentityUserDto> ObterPorEmailAsync(string email, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario is null)
                return null;

            return new IdentityUserDto(usuario.Id, usuario.Email, usuario.Nome, usuario.Ativo);
        }

        public async Task<bool> ValidarSenhaAsync(string userId, string senha, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario is null)
                return false;

            return await _userManager.CheckPasswordAsync(usuario, senha);
        }
    }
}
