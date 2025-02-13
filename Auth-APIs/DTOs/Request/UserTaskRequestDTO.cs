using Auth_APIs.Entities;
using System.ComponentModel.DataAnnotations;

namespace Auth_APIs.DTOs.Request
{
    public class UserTaskRequestDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
