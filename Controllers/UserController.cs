using backend.Repository;
using Microsoft.AspNetCore.Mvc;


/// Using CQRS pattern
namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase

    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync(cancellationToken);
            return Ok(users);
        }
    }
}
