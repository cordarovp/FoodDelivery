using FoodDelivery.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
