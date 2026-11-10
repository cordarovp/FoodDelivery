using FoodDelivery.Data;
using FoodDelivery.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public UserRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUserList()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User? GetUserId(int id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
