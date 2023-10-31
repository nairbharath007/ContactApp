using ContactApp.Models;

namespace ContactApp.Repository
{
    public interface IContactRepository
    {
        public List<Contact> GetAll();
        public Contact GetById(int id);

    }
}
