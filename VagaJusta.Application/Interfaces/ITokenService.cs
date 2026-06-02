using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(string userId, string email);
    }
}
