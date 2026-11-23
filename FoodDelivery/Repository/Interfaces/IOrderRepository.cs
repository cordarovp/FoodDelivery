using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<int, Order>
    {
        Task<IEnumerable<Order>> GetAllWithDetails();
        Task<Order?> GetByIdWithDetails(int id);
    }
}
