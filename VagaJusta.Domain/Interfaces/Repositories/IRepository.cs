using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagaJusta.Domain.Interfaces.Repositories
{
    public interface IRepository
    {
        public Task<T?> ObterPorIdAsync<T>(Guid id, CancellationToken cancellationToken) where T : class;
    }
}
