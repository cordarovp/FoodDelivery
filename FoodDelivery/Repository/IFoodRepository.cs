using FoodDelivery.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Repository
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetFoodList();
        Task<Food?> GetFoodByIdAsync(int id);
        Task AddFoodAsync(Food food);
        void UpdateFood(Food food);
        void RemoveFood(Food food);
        Task<bool> SaveChangesAsync();
    }
}
