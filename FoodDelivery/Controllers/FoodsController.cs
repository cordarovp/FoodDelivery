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
        public async Task<ActionResult<Food>> GetFoodId(int id)
        {
            var existingFood = await _service.GetFoodByIdAsync(id);
            if(existingFood == null) return NotFound($"Food by id: {id} vas not found.");

            return Ok(existingFood);
        }

        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            var createdFood = await _service.PostFoodAsync(food);
            if(createdFood == null) return BadRequest($"Food data 'Price': {food.Price} is incorect!");

            return CreatedAtAction(nameof(GetFoodId), new {id = food.Id}, food);
        }

        [HttpPut]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            var success = await _service.PutFood(id, food);

            if (!success) return BadRequest();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFood(int id) 
        { 
            var success = await _service.DeleteFood(id);

            if(!success) return NotFound();

            return NoContent();
        }
    }
}
