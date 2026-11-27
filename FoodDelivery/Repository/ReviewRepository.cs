using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class ReviewRepository : Repository<int, Review>, IReviewRepository
    {
        public ReviewRepository(FoodDeliveryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetAllWithDetails()
        {
            return await _dbSet.AsNoTracking()
                .IgnoreQueryFilters()
                .Include(r => r.User)
                .Include(r => r.Food)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdWithDetails(int id)
        {
            return await _dbSet.AsNoTracking()
                .IgnoreQueryFilters()
                .Include(r => r.User)
                .Include(r => r.Food)
                .Where(r => !r.IsDeleted)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
