using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface IMatriculaRepository : IRepository<Matricula>
    {
        Task<bool> AlunoTemMatriculaAtivaAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Matricula>> ListarFilaDeEsperaAsync(Guid turmaId, CancellationToken cancellationToken);
        Task AdicionarASync(Matricula matricula, CancellationToken cancellationToken);
    }
}
