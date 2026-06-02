using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Infrastructure.Data;
using VagaJusta.Infrastructure.Identity;

namespace VagaJusta.Infrastructure.Seed
{
    public class DatabaseSeeder(DBContext context, UserManager<UsuarioIdentity> userManager)
    {
        private readonly DBContext _context = context;
        private readonly UserManager<UsuarioIdentity> _userManager = userManager;

        public async Task SeedAsync() 
        {
            const string email = "gui1andre89@gmail.com";
            if(await _userManager.FindByEmailAsync(email) is null)
            {
                var user = new UsuarioIdentity
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Ativo = true
                };

                var result = await _userManager.CreateAsync(user, "Senha123@");

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create seed user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
