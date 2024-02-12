using Entities;
using Entities.Users;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}