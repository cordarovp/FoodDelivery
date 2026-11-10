using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery.Service
{
    public class FoodService : IFoodService
    {
        private readonly IRepository<int, Food> _repository;

        public FoodService(IRepository<int, Food> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Food> GetFoodList()
        {
            return _repository.GetAll();
        }

        public Food? GetFoodById(int id)
        {
            return _repository.GetById(id);
        }

        public Food? PostFood(Food food)
        {
            if (food == null) return null;
            if (food.Price <= 0) return null;
            if (food.Id != 0) return null;

            _repository.Add(food);
            return food;
        }

        public Food? PutFood(int id, Food food)
        {
            var existingFood = _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingFood == null || id != food.Id) return null;
            if (food.Price <= 0) return null;

            _repository.Update(food);
            return food;
        }

        public bool DeleteFood(int id)
        {
            var existingFood = _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingFood == null) return false;

            _repository.Delete(existingFood);
            return true;
        }
    }
}
