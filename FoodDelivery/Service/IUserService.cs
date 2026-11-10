using FoodDelivery.Entity;

namespace FoodDelivery.Service
{
    public interface IUserService
    {
        IEnumerable<ResponseUserDTO> GetUserDTOList();
        ResponseUserDTO? GetUserDTOById(int id);
        ResponseUserDTO? PostUser(RequestUserDTO requestUser);
        User? PutUser(int id, RequestUserDTO requestUser);
        bool DeleteUserint(int id);
    }
}
