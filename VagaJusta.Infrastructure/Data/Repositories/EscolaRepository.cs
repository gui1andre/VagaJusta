using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Infrastructure.Data.Repositories
{
    internal class EscolaRepository(DBContext context) : IEscolaRepository
    {
        private readonly DBContext _context = context;

        public async Task AdicionarAsync(Escola escola, CancellationToken cancellationToken)
        {
            await _context.Escolas.AddAsync(escola, cancellationToken);
        }

        public Task<Escola?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Escolas.FirstOrDefaultAsync(x  => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Escola>> ObterAsync(int pagina = 1, int tamanhoPagina = 10, CancellationToken cancellationToken = default)
        {
            return await _context.Escolas
                .Include(x => x.Turmas)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public void RemoverAsync(Escola entity, CancellationToken cancellationToken)
        {
            _context.Escolas.Remove(entity);
        }
    }
}
