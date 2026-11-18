using FoodDelivery.DTO.Restaurant;
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
            
            return ControllerExtensions.ToActionResult(this, existingRestaurant);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseRestaurantDTO>> PostRestaurant(RequestRestaurantDTO restaurant)
        {
            var createdRestaurant = await _service.PostRestaurant(restaurant);
            
            if (createdRestaurant.IsFailure) return ControllerExtensions.ToActionResult(this, createdRestaurant);
            return ControllerExtensions.ToCreatedAtActionResult(this, createdRestaurant, nameof(GetRestaurantById), new {id = createdRestaurant.Value!.Id});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RequestRestaurantDTO restaurant)
        {
            var success = await _service.PutRestaurant(id, restaurant);
            
            return ControllerExtensions.ToActionResult(this, success);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _service.DeleteRestaurant(id);
            
            return ControllerExtensions.ToActionResult(this, success);
        }
    }
}
