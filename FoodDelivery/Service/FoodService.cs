using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Service
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _repository;

        public FoodService(IFoodRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Food> GetFoodList()
        {
            return _repository.GetFoodList();
        }

        public Food? GetFoodById(int id)
        {
            return _repository.GetFoodById(id);
        }

        public Food? PostFood(Food food)
        {
            //Todo checks and returns of correct type for status codes
            if (food == null) return null;
            if (food.Price <= 0) return null;
            if (food.Id != 0) return null;

            _repository.AddFood(food);
            return food;
        }

        public Food? PutFood(int id, Food food)
        {
            var existingFood = _repository.GetFoodById(id);
            //Todo checks and returns of correct type for status codes
            if (existingFood == null || id != food.Id) return null;
            if (food.Price <= 0) return null;

            _repository.UpdateFood(food);
            return food;
        }

        public bool DeleteFood(int id)
        {
            var existingFood = _repository.GetFoodById(id);
            //Todo checks and returns of correct type for status codes
            if (existingFood == null) return false;

            _repository.RemoveFood(existingFood);
            return true;
        }
    }
}
