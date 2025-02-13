using Auth_APIs.DTOs.Request;
using Auth_APIs.DTOs.Response;
using Auth_APIs.Entities;
using Auth_APIs.IRepositories;
using Auth_APIs.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth_APIs.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAuthenticationRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<UserResponseDTO> Register(RegisterRequestDTO request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User 
            {
                Name = request.Name, 
                Email = request.Email, 
                PasswordHash = hashedPassword,
                Role = request.Role,
            };
            var addedUser = await _authRepository.AddUser(user);
            return new UserResponseDTO()
            {
                Id = addedUser.Id,
                Name = addedUser.Name,
                Email = addedUser.Email,
                Role = addedUser.Role,
            };
        }

        public async Task<string> Login(LoginRequestDTO request)
        {
            var user = await _authRepository.GetUserByEmail(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");
            return GenerateToken(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsers()
        {
            var userList = await _authRepository.GetAllUsers();
            return userList.Select(user => new UserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            });
        }
        public async Task<UserResponseDTO> GetUserById(int id)
        {
            var user = await _authRepository.GetUserById(id);
            return new UserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            };
        }

        public async Task<UserResponseDTO> GetUserByEmail(string email)
        {
            var user = await _authRepository.GetUserByEmail(email);
            return new UserResponseDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            };
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()) 
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
