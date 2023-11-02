using ContactApp.Models;

namespace ContactApp.Repository
{
    public interface IUserRepository
    {
        public List<User> GetAll();
        public User GetById(int id);
        public int Add(User user);
        public User Update(User updatedUser, User oldUser);
        public void Delete(User user);


    }
}
