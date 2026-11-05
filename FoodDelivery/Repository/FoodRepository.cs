using FoodDelivery.Data;
using FoodDelivery.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public FoodRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> GetFoodListAsync()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<Food?> GetFoodByIdAsync(int id)
        {
            return await _context.Foods.FindAsync(id);
        }

        public async Task PostFoodAsync(Food food)
        {
            await _context.Foods.AddAsync(food);
        }

        public void PutFoodAsync(Food food)
        {
            _context.Foods.Update(food);
        }

        public void DeleteFoodAsync(Food food)
        {
            _context.Foods.Remove(food);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
