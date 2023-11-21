using RedbrowBackendTest.DTOs;

namespace RedbrowBackendTest.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedUsersDTO> GetAll(int pageNumber, int pageSize);
        Task<UsuarioDTO> CreateUser(UsuarioDTO model);
    }
}
