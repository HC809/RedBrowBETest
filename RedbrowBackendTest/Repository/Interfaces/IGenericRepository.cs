using Microsoft.EntityFrameworkCore.ChangeTracking;
using RedbrowBackendTest.Models;
using System.Linq.Expressions;

namespace RedbrowBackendTest.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PagedResult<TEntity>> GetAllAsync(int pageNumber, int pageSize);
        Task<bool> ExistEntityByFilterAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> InsertAsync(TEntity entity);
    }
}
