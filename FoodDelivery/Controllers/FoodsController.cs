using FoodDelivery.Entity;
using FoodDelivery.Repository;
using FoodDelivery.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly FoodService _service;

        public FoodsController(FoodService service, FoodRepository repository)
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
        public async Task<ActionResult<Food>> GetFoodId(int id)
        {
            var existingFood = await _service.GetFoodById(id);
            if(existingFood == null) return NotFound($"Food by id: {id} vas not found.");

            return Ok(existingFood);
        }

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            await _service.PostFood(food);

            return CreatedAtAction(nameof(GetFoodId), new {id = food.Id}, food);
        }

        [HttpPut]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            var success = await _service.PutFoodAsync(id, food);

            if (!success) return BadRequest();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFood(int id) 
        { 
            var success = await _service.DeleteFoodAsync(id);

            if(success) return NotFound();

            return NoContent();
        }
    }
}
