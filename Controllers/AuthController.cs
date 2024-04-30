using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var token = await _authService.Login(loginDto.UserName, loginDto.Password, cancellationToken);

            if (token == null) return Unauthorized();
            return Ok(new { token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            var registeredUser = await _authService.Register(user, registerDto.Password, cancellationToken);
            var userToReturn = new UserToReturnDto(registeredUser.FirstName, registeredUser.LastName, registeredUser.Email, registeredUser.PhoneNumber);
            return Ok(userToReturn);
        }
    }
}
