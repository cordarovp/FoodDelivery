using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class FoodRepository : Repository<int, Food>, IFoodRepository
    {
        public FoodRepository(FoodDeliveryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Food>> GetAllWithReview()
        {
            return await _dbSet.AsNoTracking()
                .Include(f => f.Reviews)
                .ToListAsync();
        }

        public async Task<Food?> GetByIdWithReview(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(f => f.Reviews)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
