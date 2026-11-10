using FoodDelivery.Entity;

namespace FoodDelivery.Service
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUserDTOList();
        UserDTO? GetUserDTOById(int id);
        User? GetUserById(int id);
        UserDTO? PostUser(User user);
        User? PutUser(int id, User user);
        bool DeleteUserint(int id);
    }
}
