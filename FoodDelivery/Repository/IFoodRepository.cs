using FoodDelivery.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Repository
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetFoodListAsync();
        Task<Food?> GetFoodByIdAsync(int id);
        Task PostFoodAsync(Food food);
        void PutFoodAsync(Food food);
        void DeleteFoodAsync(Food food);
        Task<bool> SaveChangesAsync();
    }
}
