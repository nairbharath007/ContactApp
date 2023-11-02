using ContactApp.Data;
using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Contacts
                .Include(c => c.ContactDetails)
                .Where(contact => contact.IsActive == true).ToList();
        }

        public Contact GetById(int id)
        {
            return _context.Contacts
                .Include(c => c.ContactDetails)
                .Where(contact => contact.ContactId == id && contact.IsActive == true)
                .FirstOrDefault();
        }
        public int Add(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact.ContactId;

        }
        
        public Contact Update(Contact updatedContact, Contact oldContact)
        {
            _context.Entry(oldContact).State = EntityState.Detached;
            _context.Contacts.Update(updatedContact);
            _context.SaveChanges();
            return updatedContact;
        }
        public void Delete(Contact contact)
        { 
            var detailsToDelete = _context.Details.Where(d => d.ContactId == contact.ContactId).ToList();
            _context.Details.RemoveRange(detailsToDelete);

            contact.IsActive = false;
            _context.SaveChanges();
        }
    }
}
