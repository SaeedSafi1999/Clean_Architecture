using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Database
{
    public interface IApplicationDataContext:IDisposable,IAsyncDisposable
    {
        DbSet<Product> Products { get; set; }
        DbSet<Company> Companies{ get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
