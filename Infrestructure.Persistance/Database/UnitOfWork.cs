using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Extensions;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrestructure.Persistance.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Database
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly Core.Application.Database.IApplicationDataContext _context;
        private Hashtable _repositories;
        private ProductRepository _productRepository;
        private CompanyRepository _CompanyRepository;
        private UserRepository _userRepository;

        public UnitOfWork(Core.Application.Database.IApplicationDataContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public ICompanyRepository CompanyRepository => _CompanyRepository ??= new(_context);
        public IProductRepository ProductRepository => _productRepository ??= new(_context);
        public IUserRepository UserRepository => _userRepository ??= new(_context);

        public int Commit() => _context.SaveChanges();


        public async Task<int> CommitAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);


        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }
    }
}
