using Auth_APIs.Enum;

namespace Auth_APIs.DTOs.Response
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
