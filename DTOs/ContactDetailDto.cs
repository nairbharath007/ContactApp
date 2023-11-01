namespace ContactApp.DTOs
{
    public class ContactDetailDto
    {
        public int DetailId { get; set; }
        public string Type { get; set; }
        public string NumberOrEMail { get; set; }
        public int ContactId { get; set; }
    }
}
