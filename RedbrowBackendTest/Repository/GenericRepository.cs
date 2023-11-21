using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RedbrowBackendTest.DBContext;
using RedbrowBackendTest.Models;
using RedbrowBackendTest.Repository.Interfaces;
using System.Linq.Expressions;

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

        public async virtual Task<bool> ExistEntityByFilterAsync(Expression<Func<TEntity, bool>> filter)
                => await dbSet.AnyAsync(filter);

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
