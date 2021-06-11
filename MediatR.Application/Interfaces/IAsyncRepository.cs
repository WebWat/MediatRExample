using MediatR.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediatR.Application.Interfaces
{
    public interface IAsyncRepository<T> where T: BaseEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> FindByIdAsync(int id);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetListAsync();
    }
}
