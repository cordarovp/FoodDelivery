using FoodDelivery.DTO.User;
using FoodDelivery.Result;

namespace FoodDelivery.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ResponseUserDTO>> GetUserDTOList();
        Task<Reply<ResponseUserDTO>> GetUserDTOById(int id);
        Task<Reply<ResponseUserDTO>> PostUser(RequestUserDTO requestUser);
        Task<Reply> PutUser(int id, RequestUserDTO requestUser);
        Task<Reply> DeleteUser(int id);
    }
}
