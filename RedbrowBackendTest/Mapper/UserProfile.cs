using AutoMapper;
using RedbrowBackendTest.DTOs;
using RedbrowBackendTest.Entities;

namespace RedbrowBackendTest.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
