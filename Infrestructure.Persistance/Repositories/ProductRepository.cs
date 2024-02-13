using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.DTOs.Product;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IApplicationDataContext context) : base(context) { }
    }
}
