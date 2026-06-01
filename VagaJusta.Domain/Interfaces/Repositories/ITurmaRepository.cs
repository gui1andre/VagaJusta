using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        Task AdicionarAsync(Turma turma, CancellationToken cancellationToken);
    }
}
