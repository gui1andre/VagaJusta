using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepository
    {
        Task<Aluno?> ObterPorCPFAsync(string cpf, CancellationToken cancellationToken);
        Task AdicionarAlunoAsync(Aluno aluno, CancellationToken cancellationToken);
    }
}
