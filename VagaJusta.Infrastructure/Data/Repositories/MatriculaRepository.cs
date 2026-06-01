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
    public class MatriculaRepository(DBContext context) : IMatriculaRepository
    {
        private readonly DBContext _context = context;

        public async Task AdicionarASync(Matricula matricula, CancellationToken cancellationToken)
        {
            await _context.Matriculas.AddAsync(matricula, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AlunoTemMatriculaAtivaAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Matriculas.AnyAsync(x => 
            x.AlunoId == id && 
            x.Status != StatusMatriculaEnum.Cancelada &&
            x.Status != StatusMatriculaEnum.Rejeitada,
            cancellationToken);
        }

        public async Task<IEnumerable<Matricula>> ListarFilaDeEsperaAsync(Guid turmaId, CancellationToken cancellationToken)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Where(m => m.TurmaId == turmaId && m.Status == StatusMatriculaEnum.AguardandoVaga)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Matricula?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma).ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
    }
}
