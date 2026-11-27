using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IReviewRepository : IRepository<int, Review>
    {
        Task<IEnumerable<Review>> GetAllWithDetails();
        Task<Review?> GetByIdWithDetails(int id);
    }
}
