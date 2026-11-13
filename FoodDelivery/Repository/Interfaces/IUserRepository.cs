using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IUserRepository : IRepository<int, User>
    {
        Task<bool> CheckUsername(string username);
    }
}
