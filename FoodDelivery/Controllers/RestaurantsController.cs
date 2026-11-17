using FoodDelivery.DTO.Restaurant;
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
        public async Task<ActionResult<IEnumerable<ResponseRestaurantDTO>>> GetRestaurant()
        {
            var restaurantsList = await _service.GetRestaurantList();
            return Ok(restaurantsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseRestaurantDTO>> GetRestaurantById(int id)
        {
            var existingRestaurant = await _service.GetRestaurantById(id);
            if (existingRestaurant.IsFailure)
                return StatusCode(existingRestaurant.Error!.StatusCode, existingRestaurant.Error);

            return Ok(existingRestaurant.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseRestaurantDTO>> PostRestaurant(RequestRestaurantDTO restaurant)
        {
            var createdRestaurant = await _service.PostRestaurant(restaurant);
            if (createdRestaurant.IsFailure) 
                return StatusCode(createdRestaurant.Error!.StatusCode, createdRestaurant.Error);

            return CreatedAtAction(nameof(GetRestaurantById), new { id = createdRestaurant.Value!.Id }, createdRestaurant.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RequestRestaurantDTO restaurant)
        {
            var success = await _service.PutRestaurant(id, restaurant);
            if (success.IsFailure) 
                return StatusCode(success.Error!.StatusCode, success.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _service.DeleteRestaurant(id);
            if (success.IsFailure) 
                return StatusCode(success.Error!.StatusCode, success.Error);

            return NoContent();
        }
    }
}
