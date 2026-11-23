using FoodDelivery.DTO.Order;
using FoodDelivery.Result;

namespace FoodDelivery.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<ResponseOrderDTO>> GetOrderList();
        Task<Reply<ResponseOrderDTO>> GetOrderById(int id);
        Task<Reply<ResponseOrderDTO>> PostOrder(RequestOrderDTO order);
        //Should updating orders be available?
        //Task<Reply> PutOrder(int id, RequestOrderDTO order);
        Task<Reply> DeleteOrder(int id);
    }
}
