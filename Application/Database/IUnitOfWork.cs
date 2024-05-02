using Core.Application.Common;
using Core.Application.Repositories;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Database
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
