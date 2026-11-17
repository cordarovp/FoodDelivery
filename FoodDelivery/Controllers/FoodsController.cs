using FoodDelivery.DTO.Food;
using FoodDelivery.Entity;
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

            if (existingFood.IsFailure)
                return StatusCode(existingFood.Error!.StatusCode, existingFood.Error);

            return Ok(existingFood.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseFoodDTO>> PostFood(RequestFoodDTO requestFood)
        {
            var createdFood = await _service.PostFood(requestFood);
            if (createdFood.IsFailure)
                return StatusCode(createdFood.Error!.StatusCode, createdFood.Error);

            return CreatedAtAction(nameof(GetFoodById), new { id = createdFood.Value!.Id }, createdFood.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, RequestFoodDTO requestFood)
        {
            var success = await _service.PutFood(id, requestFood);

            if (success.IsFailure)
                return StatusCode(success.Error!.StatusCode, success.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var success = await _service.DeleteFood(id);

            if (success.IsFailure)
                return StatusCode(success.Error!.StatusCode, success.Error);

            return NoContent();
        }
    }
}
