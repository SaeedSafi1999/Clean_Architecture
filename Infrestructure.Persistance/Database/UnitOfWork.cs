using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.Entities;
using Infrestructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Database
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDataContext _context;

        private ProductRepository _productRepository;
        private CompanyRepository _CompanyRepository;

        public UnitOfWork(IApplicationDataContext context)
        {
            _context = context;
        }

        public ICompanyRepository CompanyRepository => _CompanyRepository ??= new(_context);
        public IProductRepository ProductRepository =>_productRepository??= new(_context);

        public  int Commit() =>  _context.SaveChanges();

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
    }
}
