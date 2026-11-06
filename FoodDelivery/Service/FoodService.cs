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
            if (food.Price <= 0) return null;

            _repository.AddFood(food);
            _repository.SaveChanges();
            return food;
        }

        public Food? PutFood(int id, Food food)
        {
            var existingFood = _repository.GetFoodById(id);
            if (existingFood == null || id != food.Id)
            {
                return null;
            }

            _repository.UpdateFood(food);
            _repository.SaveChanges();
            return food;
        }

        public bool DeleteFood(int id)
        {
            var existingFood = _repository.GetFoodById(id);
            if(existingFood == null)
            {
                return false;
            }

            _repository.RemoveFood(existingFood);
            return _repository.SaveChanges();
        }
    }
}
