using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class OrderRepository : Repository<int, Order>, IOrderRepository
    {
        public OrderRepository(FoodDeliveryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllWithDetails()
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Food)
                .Include(o => o.Restaurant)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdWithDetails(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.Food)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
