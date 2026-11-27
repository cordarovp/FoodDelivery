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
                .IgnoreQueryFilters()
                .Include(o => o.User)
                .Include(o => o.Food)
                .Include(o => o.Restaurant)
                .Where(o => !o.IsDeleted)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdWithDetails(int id)
        {
            return await _dbSet.AsNoTracking()
                .IgnoreQueryFilters()
                .Include(o => o.User)
                .Include(o => o.Food)
                .Include(o => o.Restaurant)
                .Where(o => !o.IsDeleted)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
