using FoodDelivery.DTO.User;
using FoodDelivery.Result;

namespace FoodDelivery.Service.Interfaces
{
    public interface IAuthService
    {
        Task<Reply<ResponseUserDTO>> Register(RequestUserDTO registerUser);
        Task<Reply<string?>> Login(LoginUserDTO loginUser);
    }
}
