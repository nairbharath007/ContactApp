using ContactApp.Models;

namespace ContactApp.Repository
{
    public interface IContactDetailRepository
    {
        public List<ContactDetail> GetAll();
        public ContactDetail GetById(int id);


    }
}
