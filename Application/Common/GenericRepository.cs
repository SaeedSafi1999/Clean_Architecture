using Core.Application.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IApplicationDataContext Context;

        public GenericRepository(IApplicationDataContext context)
        {
            Context = context;
        }

        public async Task<bool> AddAsync(T Entity)
        {
            try
            {
                await Context.Set<T>().AddAsync(Entity);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> AddRangeAsync(List<T> Entities)
        {
            try
            {
                await Context.Set<T>().AddRangeAsync(Entities);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken deafult) => await Context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(params object[] keyValues) => await Context.Set<T>().FindAsync(keyValues);

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var data = await GetByIdAsync(id);
                Context.Set<T>().Remove(data);
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool Update(T Entity)
        {
            try
            {
                Context.Set<T>().Update(Entity);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }

        public bool UpdateRangeAsync(List<T> Entities)
        {
            try
            {
                Context.Set<T>().UpdateRange(Entities);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
