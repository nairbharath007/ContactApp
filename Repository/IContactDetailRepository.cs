using ContactApp.Models;

namespace ContactApp.Repository
{
    public interface IContactDetailRepository
    {
        public List<ContactDetail> GetAll();
        public ContactDetail GetById(int id);
        public int Add(ContactDetail contactDetail);
        public ContactDetail Update(ContactDetail updatedContactDetail, ContactDetail oldContactDetail);
        /*public void Delete(ContactDetail contactDetail);*/
        /*public bool Delete(int id);*/
        public void Delete(int contactDetailId);


    }
}
