using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IUserRepository : IRepository<int, User>
    {
        Task<User?> FindUserWithUsername(string username);
    }
}
