using Core.Domain.Entities;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Database
{
    public interface IApplicationDataContext:IDisposable,IAsyncDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
