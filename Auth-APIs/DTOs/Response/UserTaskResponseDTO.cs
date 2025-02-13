using Auth_APIs.Entities;
using System.ComponentModel.DataAnnotations;

namespace Auth_APIs.DTOs.Response
{
    public class UserTaskResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
