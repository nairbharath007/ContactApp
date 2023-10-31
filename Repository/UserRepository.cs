using ContactApp.Data;
using ContactApp.Models;
using ContactApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace ContactApp1.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context)
        {
            _context = context;
        }
        public List<User> GetAll()
        {
            return _context.Users.Where(user => user.IsActive==true)
/*                .Include(user => user.Contacts)
*/                .ToList();
        }
        public User GetById(int id)
        {
            return _context.Users.Where(user => user.UserId == id && user.IsActive==true)
                .FirstOrDefault();
        }
        public int Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var newUserId = _context.Users.Where(user1 => user1.FirstName == user.FirstName)
                .OrderBy(usr => usr.UserId).LastOrDefault().UserId;
            return newUserId;
        }
        public User Update(User user)
        {
            var userToUpdate = GetById(user.UserId);
            if(userToUpdate != null)
            {
                _context.Entry(userToUpdate).State = EntityState.Detached;
                _context.Users.Update(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }
        public bool Delete(int id) //basically a soft update 
        {
            var userToDelete = GetById(id);
            if(userToDelete != null)
            {
                userToDelete.IsActive = false;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
