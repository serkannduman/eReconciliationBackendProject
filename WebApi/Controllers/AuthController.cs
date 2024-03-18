using Business.Abstract;
using Entities.Concrete;
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
        public IActionResult Register(UserAndCompanyRegisterDto userAndCompanyRegisterDto)
        {
            var userExists = _authService.UserExists(userAndCompanyRegisterDto.UserForRegister.Email);
            if (!userExists.Success)
                return BadRequest(userExists.Message);  

            var companyExists = _authService.CompanyExists(userAndCompanyRegisterDto.Company);
            if (!companyExists.Success)
                return BadRequest(companyExists.Message);

            var registerResult = _authService.Register(userAndCompanyRegisterDto.UserForRegister, userAndCompanyRegisterDto.UserForRegister.Password, userAndCompanyRegisterDto.Company);

            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);

            if (result.Success)
                return Ok(result.Data);

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
