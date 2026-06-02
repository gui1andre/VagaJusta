using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.ValueObjects;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<Aluno?> ObterPorCPFAsync(CPF cpf, CancellationToken cancellationToken);
        Task AdicionarAlunoAsync(Aluno aluno, CancellationToken cancellationToken);
        Task AtualizarAlunoAsync(CancellationToken cancellationToken);
    }
}
