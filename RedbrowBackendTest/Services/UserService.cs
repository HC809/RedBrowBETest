using AutoMapper;
using RedbrowBackendTest.DTOs;
using RedbrowBackendTest.Entities;
using RedbrowBackendTest.Models;
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

        public async Task<PagedUsersDTO> GetAll(int pageNumber, int pageSize)
        {
            PagedResult<Usuario> pagedResult = await _userRepository.GetAllAsync(pageNumber, pageSize);

            return new PagedUsersDTO()
            {
                Items = _mapper.Map<List<UsuarioDTO>>(pagedResult.Items),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };
        }

        public async Task<UsuarioDTO> CreateUser(UsuarioDTO model)
        {
            await ValidateModel(model);

            Usuario userEntity = await _userRepository.InsertAsync(_mapper.Map<Usuario>(model));

            return _mapper.Map<UsuarioDTO>(userEntity);
        }

        public async Task<bool> ExistUserByEmail(string email)
            => await _userRepository.ExistEntityByFilterAsync(q => q.Correo.Trim() == email.Trim());

        private async Task ValidateModel(UsuarioDTO model)
        {
            var message = "El campo '{0}' del modelo no puede ser nulo o vacío.";

            if (string.IsNullOrWhiteSpace(model.Nombre))
                throw new ArgumentException(string.Format(message, nameof(model.Nombre)), nameof(model.Nombre));

            if (string.IsNullOrWhiteSpace(model.Correo))
                throw new ArgumentException(string.Format(message, nameof(model.Correo)), nameof(model.Correo));

            if (model.Edad <= 0)
                throw new ArgumentException("La 'Edad' debe ser mayor que cero.", nameof(model.Edad));

            if (await ExistUserByEmail(model.Correo))
                throw new ArgumentException($"Ya existe un usuario con el correo electrónico '{model.Correo}'.");
        }
    }
}
