using FoodDelivery.Entity;

namespace FoodDelivery.Service
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetFoodList();
        Task<Food?> GetFoodById(int id);
        Task<Food> PostFood(Food food);
        Task<bool> PutFoodAsync(int id, Food food);
        Task<bool> DeleteFoodAsync(int id);
    }
}
