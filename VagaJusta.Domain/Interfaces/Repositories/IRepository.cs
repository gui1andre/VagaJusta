using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<TEntity?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
