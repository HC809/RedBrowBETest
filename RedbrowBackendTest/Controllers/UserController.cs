using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios() => Ok(await _userService.GetAll());
    }
}
