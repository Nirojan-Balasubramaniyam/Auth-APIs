using Auth_APIs.DTOs.Request;
using Auth_APIs.DTOs.Response;
using Auth_APIs.Entities;

namespace Auth_APIs.IService
{
    public interface IAuthenticationService
    {
        Task<UserResponseDTO> Register(RegisterRequestDTO request);
        Task<string> Login(LoginRequestDTO request);
        Task<IEnumerable<UserResponseDTO>> GetAllUsers();
        Task<UserResponseDTO> GetUserById(int id);
    }
}
