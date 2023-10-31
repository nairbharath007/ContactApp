using ContactApp.Data;
using ContactApp.Models;

namespace ContactApp.Repository
{
    public class ContactRepository: IContactRepository
    {
        private readonly MyContext _context;

        public ContactRepository(MyContext context)
        {
            _context = context;
        }

        public List<Contact> GetAll()
        {
            return _context.Contacts.Where(contact => contact.IsActive == true).ToList();
        }

        public Contact GetById(int id)
        {
            return _context.Contacts.FirstOrDefault(contact => contact.ContactId == id);
        }
    }
}
