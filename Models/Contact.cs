using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApp.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public List<ContactDetail> ContactDetails { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
