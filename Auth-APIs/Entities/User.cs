using System.ComponentModel.DataAnnotations;
using Auth_APIs.Enum;

namespace Auth_APIs.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public List<UserTask> UserTasks { get; set; } = new List<UserTask>();
    }
}
