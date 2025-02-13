using System.Security.Claims;
using Auth_APIs.DTOs.Request;
using Auth_APIs.Enum;
using Auth_APIs.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO request)
        {
            var addedUser = await _authService.Register(request);
            return Ok(new { message = "User registered successfully", addedUser });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var token = await _authService.Login(request);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _authService.GetAllUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);  // Get email from JWT token
            var loggedInUser = await _authService.GetUserByEmail(userEmail);

            if (loggedInUser.Role == UserRole.Admin || loggedInUser.Id == id)
            {
                var user = await _authService.GetUserById(id);
                return Ok(user);
            }

            return Forbid();  // Restrict access if not admin or the same user
        }
    }
}
