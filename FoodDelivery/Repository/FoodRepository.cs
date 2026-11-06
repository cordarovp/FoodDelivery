using FoodDelivery.Data;
using FoodDelivery.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FoodDelivery.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public FoodRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Food> GetFoodList()
        {
            return _context.Foods.ToList();
        }

        public Food? GetFoodById(int id)
        {
            return _context.Foods.Find(id);
        }

        public void AddFood(Food food)
        {
            _context.Foods.Add(food);
        }

        public void UpdateFood(Food food)
        {
            _context.Foods.Update(food);
        }

        public void RemoveFood(Food food)
        {
            _context.Foods.Remove(food);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
