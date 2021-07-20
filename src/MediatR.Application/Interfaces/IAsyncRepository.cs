using MediatR.Application.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Application.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken = default);
    }
}
