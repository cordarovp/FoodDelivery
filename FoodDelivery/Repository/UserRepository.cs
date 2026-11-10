using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;

namespace FoodDelivery.Repository
{
    public class UserRepository : Repository<int, User>, IUserRepository
    {
        public UserRepository(FoodDeliveryDbContext context) : base(context)
        {
        }

        public bool CheckUsername(string username)
        {
            return _dbSet.Any(u => u.Username.ToLower() == username.ToLower());
        }
    }
}
