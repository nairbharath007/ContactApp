using ContactApp.Models;

namespace ContactApp.Repository
{
    public interface IContactRepository
    {
        public List<Contact> GetAll();
        public Contact GetById(int id);
        public int Add(Contact contact);
        public Contact Update(Contact updatedContact, Contact oldContact);
        public void Delete(Contact contact);


    }
}
