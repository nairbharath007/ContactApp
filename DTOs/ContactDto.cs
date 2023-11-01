using System.ComponentModel.DataAnnotations;

namespace ContactApp.DTOs
{
    public class ContactDto
    {
        public int ContactId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public List<ContactDetailDto>? ContactDetails { get; set; }
        public int UserId { get; set; }
    }
}
