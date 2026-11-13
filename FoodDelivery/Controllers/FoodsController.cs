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
        public async Task<ActionResult<IEnumerable<Food>>> GetFood()
        {
            var foodList = await _service.GetFoodList();
            return Ok(foodList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFoodById(int id)
        {
            var existingFood = await _service.GetFoodById(id);
            if (existingFood == null) return NotFound($"Food by id: {id} was not found.");

            return Ok(existingFood);
        }

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            var createdFood = await _service.PostFood(food);
            if (createdFood == null) return BadRequest($"Food data 'Price': {food.Price} is incorect or id was not suppose to be set!");

            return CreatedAtAction(nameof(GetFoodById), new { id = food.Id }, food);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            var success = await _service.PutFood(id, food);

            if (success == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var success = await _service.DeleteFood(id);

            if (!success) return NotFound();

            return NoContent();
        }
    }
}
