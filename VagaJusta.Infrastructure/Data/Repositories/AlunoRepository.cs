using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Interfaces.Repositories;
using VagaJusta.Domain.ValueObjects;

namespace VagaJusta.Infrastructure.Data.Repositories
{
    public class AlunoRepository(DBContext context) : IAlunoRepository
    {
        private readonly DBContext _context = context;
        public async Task AdicionarAlunoAsync(Aluno aluno, CancellationToken cancellationToken)
        {
            await _context.AddAsync(aluno, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Aluno?> ObterPorCPFAsync(CPF cpf, CancellationToken cancellationToken)
        {
            return await _context.Alunos.FirstOrDefaultAsync(x => x.CPF.Numero == cpf.Numero, cancellationToken);
        }

        public async Task<Aluno?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
