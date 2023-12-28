using Core.Application.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken deafult);
        Task<T> GetByIdAsync(params object[] keyValues);
        Task<bool> AddAsync(T Entity);
        Task<bool> AddRangeAsync(List<T> Entities);
        Task<bool> RemoveAsync(int id);
        bool Update(T Entity);
        bool UpdateRangeAsync(List<T> Entities);
    }
}
