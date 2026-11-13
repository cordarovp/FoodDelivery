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

        public async Task<IEnumerable<Food>> GetFoodList()
        {
            return await _repository.GetAll();
        }

        public async Task<Food?> GetFoodById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Food?> PostFood(Food food)
        {
            if (food == null) return null;
            if (food.Price <= 0) return null;
            if (food.Id != 0) return null;

            await _repository.Add(food);
            return food;
        }

        public async Task<Food?> PutFood(int id, Food food)
        {
            var existingFood = await _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingFood == null) return null;
            if (food.Id != 0) return null;
            if (food.Price <= 0) return null;

            await _repository.Update(food);
            return food;
        }

        public async Task<bool> DeleteFood(int id)
        {
            var existingFood = await _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingFood == null) return false;

            await _repository.Delete(existingFood);
            return true;
        }
    }
}
