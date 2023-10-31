using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactApp.Models
{
    public class ContactDetail
    {
        [Key]
        public int DetailId { get; set; }
        public string Type { get; set; }
        public string NumberOrEMail { get; set; }
        public Contact Contact { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }
    }
}
