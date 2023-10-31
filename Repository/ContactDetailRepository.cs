using ContactApp.Data;
using ContactApp.Models;

namespace ContactApp.Repository
{
    public class ContactDetailRepository : IContactDetailRepository
    {
        private readonly MyContext _context;

        public ContactDetailRepository(MyContext context)
        {
            _context = context;
        }

        public List<ContactDetail> GetAll()
        {
            return _context.Details.ToList();
        }

        public ContactDetail GetById(int id)
        {
            return _context.Details.FirstOrDefault(detail => detail.DetailId == id);
        }
    }
}