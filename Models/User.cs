using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

        public List<Contact>? Contacts { get; set; }
    }
}
