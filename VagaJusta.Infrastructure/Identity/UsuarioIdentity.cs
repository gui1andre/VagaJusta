using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Infrastructure.Identity
{
    public class UsuarioIdentity : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
