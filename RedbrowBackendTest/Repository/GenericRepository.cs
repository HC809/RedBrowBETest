using Microsoft.EntityFrameworkCore;
using RedbrowBackendTest.DBContext;
using RedbrowBackendTest.Models;
using RedbrowBackendTest.Repository.Interfaces;

namespace RedbrowBackendTest.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private TestDBContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(TestDBContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<PagedResult<TEntity>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await dbSet.AsNoTracking().CountAsync();

            var items = await dbSet.AsNoTracking()
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<TEntity>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
