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
            try
            {
                var addedUser = await _authService.Register(request);
                return Ok(new { message = "User registered successfully", addedUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while registering the user", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            try
            {
                var token = await _authService.Login(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while logging in", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _authService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while fetching users", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Name);  // Get email from JWT token
                var loggedInUser = await _authService.GetUserByEmail(userEmail);

                if (loggedInUser.Role == UserRole.Admin || loggedInUser.Id == id)
                {
                    var user = await _authService.GetUserById(id);
                    return Ok(user);
                }

                return Unauthorized(new { message = "Access denied" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while fetching the user", error = ex.Message });
            }
        }

    }
}
