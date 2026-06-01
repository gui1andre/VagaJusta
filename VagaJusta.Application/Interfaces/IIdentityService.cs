using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Application.DTO;

namespace VagaJusta.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityUserDto> ObterPorEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ValidarSenhaAsync(string userId, string senha, CancellationToken cancellationToken);
    }
}
