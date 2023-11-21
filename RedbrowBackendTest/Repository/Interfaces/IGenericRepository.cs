using RedbrowBackendTest.Models;

namespace RedbrowBackendTest.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PagedResult<TEntity>> GetAllAsync(int pageNumber, int pageSize);
    }
}
