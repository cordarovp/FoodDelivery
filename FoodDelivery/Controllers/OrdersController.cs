using FoodDelivery.DTO.Order;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseOrderDTO>>> GetOrder()
        {
            var orderList = await _service.GetOrderList();
            return Ok(orderList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseOrderDTO>> GetOrderById(int id)
        {
            var existingOrder = await _service.GetOrderById(id);

            return this.ToActionResult(existingOrder);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseOrderDTO>> PostOrder(RequestOrderDTO requestOrder)
        {
            var createdOrder = await _service.PostOrder(requestOrder);

            return this.ToCreatedAtActionResult(createdOrder, nameof(GetOrderById), new { id = createdOrder.Value?.orderId });
        }

        //Should updating orders be available?
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(int id, RequestOrderDTO requestOrder)
        //{
        //    var success = await _service.PutOrder(id, requestOrder);

        //    return this.ToActionResult(success);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var success = await _service.DeleteOrder(id);
            return this.ToActionResult(success);
        }
    }
}
