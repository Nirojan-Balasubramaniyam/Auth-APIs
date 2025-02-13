using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Auth_APIs.Entities
{
    public class UserTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
