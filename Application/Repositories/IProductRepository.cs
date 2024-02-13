using Core.Application.Common;
using Core.Domain.DTOs.Product;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
    }
}
