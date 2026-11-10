using FoodDelivery.Entity;

namespace FoodDelivery.Repository
{
    public interface IFoodRepository
    {
        IEnumerable<Food> GetFoodList();
        Food? GetFoodById(int id);
        void AddFood(Food food);
        void UpdateFood(Food food);
        void RemoveFood(Food food);
    }
}
