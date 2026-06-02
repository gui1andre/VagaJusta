using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Entities;
using VagaJusta.Domain.Enums;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        Task AdicionarAsync(Turma turma, CancellationToken cancellationToken);
        Task<bool> VerificarTurmaJaExistenteAsync(Guid EscolaId, SerieEnum serie, CategoriaSerieEnum categoriaSerieEnum, CancellationToken cancellationToken);
    }
}
