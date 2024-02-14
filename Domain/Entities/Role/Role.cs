using Entities.Users;
using Entities.UsersEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Role
{
    public class Role:BaseEntity.BaseEntity
    {
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
