using MediatR.Application.Common;
using MediatR.Application.Interfaces;
using MediatR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Infrastructure.Repositories
{
    public class EFCoreRepository<T> : IAsyncDisposable, IAsyncRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;

        public EFCoreRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<T> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }


        public async Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }


        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
