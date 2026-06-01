using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface IEscolaRepository : IRepository<Escola>
    {
        Task<IEnumerable<Escola>> ObterTodasAsync(int pagina = 1, int tamanhoPagina = 10, CancellationToken cancellationToken = default);
        Task AdicionarAsync(Escola escola, CancellationToken cancellationToken);
    }
}
