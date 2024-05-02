using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Repositories;
using Entities.UsersEntity;

namespace Infrestructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IApplicationDataContext context) : base(context) { }
        
    }
}
