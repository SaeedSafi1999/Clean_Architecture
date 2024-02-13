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
        
    }
}
