using AutoMapper;
using RedbrowBackendTest.DTOs;
using RedbrowBackendTest.Entities;
using RedbrowBackendTest.Repository.Interfaces;
using RedbrowBackendTest.Services.Interfaces;

namespace RedbrowBackendTest.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Usuario> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<Usuario> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> GetAll()
        {
            return _mapper.Map<UsuarioDTO>(await _userRepository.GetAllAsync());
        }
    }
}
