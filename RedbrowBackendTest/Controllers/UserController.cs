using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedbrowBackendTest.DTOs;
using RedbrowBackendTest.Services.Interfaces;

namespace RedbrowBackendTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> ObtenerUsuarios(int pageNumber, int pageSize) => Ok(await _userService.GetAll(pageNumber, pageSize));

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioDTO user) => Ok(await _userService.CreateUser(user));
    }
}
