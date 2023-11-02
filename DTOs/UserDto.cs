using System.ComponentModel.DataAnnotations;

namespace ContactApp.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public int CountContacts { get; set; } = 0;
    }
}
