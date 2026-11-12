using FoodDelivery.Entity;

namespace FoodDelivery.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ResponseUserDTO>> GetUserDTOList();
        Task<ResponseUserDTO?> GetUserDTOById(int id);
        Task<ResponseUserDTO?> PostUser(RequestUserDTO requestUser);
        Task<ResponseUserDTO?> PutUser(int id, RequestUserDTO requestUser);
        Task<bool> DeleteUserint(int id);
    }
}
