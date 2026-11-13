using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class UserRepository : Repository<int, User>, IUserRepository
    {
        public UserRepository(FoodDeliveryDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckUsername(string username)
        {
            return await _dbSet.AnyAsync(u => u.Username.Equals(username));
        }
    }
}
