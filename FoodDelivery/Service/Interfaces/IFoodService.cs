using FoodDelivery.Entity;

namespace FoodDelivery.Service.Interfaces
{
    public interface IFoodService
    {
        IEnumerable<Food> GetFoodList();
        Food? GetFoodById(int id);
        Food? PostFood(Food food);
        Food? PutFood(int id, Food food);
        bool DeleteFood(int id);
    }
}
