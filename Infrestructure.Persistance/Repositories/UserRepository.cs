using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Core.Domain.DTOs.Product;
using Core.Domain.Entities;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IApplicationDataContext context) : base(context) { }
        public async Task<IEnumerable<ProductDTO>> GetProductsOfCompany(int CompanyId)
        {
            return await Context.Products.Where(x => x.CompanyId == CompanyId).Select(x => new ProductDTO
            {
                CompanyId = x.CompanyId,
                CreateTime = x.CreateTime,
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }
    }
}
