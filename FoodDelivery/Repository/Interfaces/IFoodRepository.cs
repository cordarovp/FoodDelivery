using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IFoodRepository : IRepository<int, Food>
    {
        Task<IEnumerable<Food>> GetAllWithReview();
        Task<Food?> GetByIdWithReview(int id);
    }
}
