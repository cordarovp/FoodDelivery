using FoodDelivery.Entity;

namespace FoodDelivery.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUserList();
        User? GetUserId(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        bool CheckUsername(string username);
    }
}
