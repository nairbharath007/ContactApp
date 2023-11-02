using ContactApp.Data;
using ContactApp.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Details
                .Where(dept => dept.DetailId == id)
                .FirstOrDefault();
        }
        public int Add(ContactDetail contactDetail)
        {
            
            _context.Details.Add(contactDetail);
            _context.SaveChanges();
            var detailsId = _context.Details.Where(det => det.Type == contactDetail.Type)
                .OrderBy(det => det.DetailId).LastOrDefault().DetailId;

            /*return contactDetail.DetailId;*/
            return detailsId;
        }
        public ContactDetail Update(ContactDetail updatedContactDetail, ContactDetail oldContactDetail)
        {
            _context.Entry(oldContactDetail).State = EntityState.Detached;
            _context.Details.Update(updatedContactDetail);
            _context.SaveChanges();
            return updatedContactDetail;
        }

        /*public void Delete(ContactDetail contactDetail)
        {
            _context.Details.Remove(contactDetail);
            _context.SaveChanges();


        }*/

        /*public bool Delete(int id)
        {
            var matchedDetails = GetById(id);
            if (matchedDetails != null)
            {
                _context.Details.Remove(matchedDetails);
                _context.SaveChanges();
                return true;
            }
            return false;
        }*/

        public void Delete(int contactDetailId)
        {
            var contactDetailToDelete = _context.Details
                .FirstOrDefault(detail => detail.DetailId == contactDetailId);

            if (contactDetailToDelete != null)
            {
                _context.Details.Remove(contactDetailToDelete);
                _context.SaveChanges();
            }
        }
    }
}