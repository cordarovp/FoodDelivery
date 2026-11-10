using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IUserRepository : IRepository<int, User>
    {
        bool CheckUsername(string username);
    }
}
