using FoodDelivery.Entity;

namespace FoodDelivery.Service
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetFoodList();
        Task<Food?> GetFoodByIdAsync(int id);
        Task<Food?> PostFoodAsync(Food food);
        Task<bool> PutFood(int id, Food food);
        Task<bool> DeleteFood(int id);
    }
}
