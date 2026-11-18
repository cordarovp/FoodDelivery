using FoodDelivery.DTO.Food;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodService _service;

        public FoodsController(IFoodService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseFoodDTO>>> GetFood()
        {
            var foodList = await _service.GetFoodList();
            return Ok(foodList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseFoodDTO>> GetFoodById(int id)
        {
            var existingFood = await _service.GetFoodById(id);

            return ControllerExtensions.ToActionResult(this, existingFood);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseFoodDTO>> PostFood(RequestFoodDTO requestFood)
        {
            var createdFood = await _service.PostFood(requestFood);

            if (createdFood.IsFailure) return ControllerExtensions.ToActionResult(this, createdFood);

            return ControllerExtensions.ToCreatedAtActionResult(this, createdFood, nameof(GetFoodById), new { id = createdFood.Value!.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, RequestFoodDTO requestFood)
        {
            var success = await _service.PutFood(id, requestFood);
            return ControllerExtensions.ToActionResult(this, success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var success = await _service.DeleteFood(id);
            return ControllerExtensions.ToActionResult(this, success);
        }
    }
}
