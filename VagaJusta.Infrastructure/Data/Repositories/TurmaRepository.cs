using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Enums;
using VagaJusta.Domain.Interfaces.Repositories;

namespace VagaJusta.Infrastructure.Data.Repositories
{
    public class TurmaRepository(DBContext context) : ITurmaRepository
    {
        private readonly DBContext _context = context;

        public async Task AdicionarAsync(Turma turma, CancellationToken cancellationToken)
        {
            await _context.Turma.AddAsync(turma);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Turma?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Turma.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task RemoverAsync(Turma entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> VerificarTurmaJaExistenteAsync(Guid escolaId,SerieEnum serie, CategoriaSerieEnum categoriaSerieEnum, CancellationToken cancellationToken)
        {
            return await _context.Turma.AnyAsync(t => t.EscolaId == escolaId && t.Serie == serie && t.CategoriaSerie == categoriaSerieEnum, cancellationToken);
        }
    }
}
