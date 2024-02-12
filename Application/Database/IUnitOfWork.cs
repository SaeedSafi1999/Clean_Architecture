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
        ICompanyRepository CompanyRepository { get; }
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
