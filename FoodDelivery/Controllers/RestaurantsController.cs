using FoodDelivery.Entity;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantsController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
            var restaurantsList = await _service.GetRestaurantList();
            return Ok(restaurantsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
        {
            var existingRestaurant = await _service.GetRestaurantById(id);
            if (existingRestaurant == null) return NotFound($"Restaurant by id: {id} was not found!");

            return Ok(existingRestaurant);
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            var createdRestaurant = await _service.PostRestaurant(restaurant);
            if (createdRestaurant == null) return BadRequest();

            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            var success = await _service.PutRestaurant(id, restaurant);
            if (success == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _service.DeleteRestaurant(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
