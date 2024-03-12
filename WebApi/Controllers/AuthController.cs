using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegister userForRegister)
        {
            var userExists = _authService.UserExists(userForRegister.Email);
            if (!userExists.Success)
                return BadRequest(userExists.Message);

            var registerResult = _authService.Register(userForRegister, userForRegister.Password);

            if (registerResult.Success)
                return Ok(registerResult);

            return BadRequest(registerResult.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.login(userForLogin);
            if (!userToLogin.Success)
                return BadRequest(userToLogin.Message);

            var result = _authService.CreateAccessToken(userToLogin.Data, 0);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }
    }
}
