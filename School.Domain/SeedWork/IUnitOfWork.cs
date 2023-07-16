using System;
using System.Threading;
using System.Threading.Tasks;

namespace School.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
