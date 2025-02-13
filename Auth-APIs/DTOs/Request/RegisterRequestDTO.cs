using Auth_APIs.Enum;

namespace Auth_APIs.DTOs.Request
{
    public class RegisterRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
