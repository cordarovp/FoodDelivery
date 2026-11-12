using FoodDelivery.Entity;

namespace FoodDelivery.Service.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetFoodList();
        Task<Food?> GetFoodById(int id);
        Task<Food?> PostFood(Food food);
        Task<Food?> PutFood(int id, Food food);
        Task<bool> DeleteFood(int id);
    }
}
