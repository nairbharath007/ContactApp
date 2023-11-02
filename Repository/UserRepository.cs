using ContactApp.Data;
using ContactApp.Models;
using ContactApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContactApp1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context)
        {
            _context = context;
        }
        public List<User> GetAll()
        {
            return _context.Users.Where(user => user.IsActive == true)
                .Include(user => user.Contacts
                .Where(contact => contact.IsActive == true))
                .ToList();
        }
        public User GetById(int id)
        {
            var user= _context.Users
                .Where(user => user.UserId == id && user.IsActive == true)
                .FirstOrDefault();
            if(user!=null)
                _context.Entry(user).State = EntityState.Detached;
            return user;

        }
        public int Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var newUserId = _context.Users.Where(user1 => user1.FirstName == user.FirstName)
                .OrderBy(user2 => user2.UserId).LastOrDefault().UserId;
            return newUserId;
        }
        public User Update(User updatedUser, User oldUser)
        {
            _context.Entry(oldUser).State = EntityState.Detached;
            _context.Users.Update(updatedUser);
            _context.SaveChanges();
            return updatedUser;
        }
        public void Delete(User user)
        {
            /*_context.Entry(user).State = EntityState.Modified;
            user.IsActive = false;
            _context.SaveChanges();*/

            /*var contactsToDelete = _context.Contacts.Where(c => c.UserId == user.UserId).ToList();

            foreach (var contact in contactsToDelete)
            {
                contact.IsActive = false;
            }
            user.IsActive = false;
            _context.SaveChanges();*/

            _context.Entry(user).State= EntityState.Modified;
            

            var contactsToDelete = _context.Contacts
                .Where(contact => contact.UserId == user.UserId);
            foreach (var contact in contactsToDelete)
            {
                
                var contactDetailsToDelete = _context.Details
                    .Where(details => details.ContactId == contact.ContactId);
                _context.Details.RemoveRange(contactDetailsToDelete);
                contact.IsActive = false;
            }
            user.IsActive = false;
            _context.SaveChanges();
        
        }
    }
}
