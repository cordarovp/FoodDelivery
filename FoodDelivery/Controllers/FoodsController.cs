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
        public ActionResult<IEnumerable<Food>> GetFood()
        {
            var foodList = _service.GetFoodList();
            return Ok(foodList);
        }

        [HttpGet("{id}")]
        public ActionResult<Food> GetFoodById(int id)
        {
            var existingFood = _service.GetFoodById(id);
            if(existingFood == null) return NotFound($"Food by id: {id} vas not found.");

            return Ok(existingFood);
        }

        [HttpPost]
        public ActionResult<Food> PostFood(Food food)
        {
            var createdFood = _service.PostFood(food);
            if(createdFood == null) return BadRequest($"Food data 'Price': {food.Price} is incorect!");

            return CreatedAtAction(nameof(GetFoodById), new {id = food.Id}, food);
        }

        [HttpPut("{id}")]
        public IActionResult PutFood(int id, Food food)
        {
            var success = _service.PutFood(id, food);

            if (success == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id) 
        { 
            var success = _service.DeleteFood(id);

            if(!success) return NotFound();

            return NoContent();
        }
    }
}
