using FoodDelivery.Entity;
using FoodDelivery.Service;
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
        public ActionResult<Restaurant> GetRestaurant()
        {
            var restaurantsList = _service.GetRestaurantList();
            return Ok(restaurantsList);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurantById(int id)
        {
            var existingRestaurant = _service.GetRestaurantById(id);
            if(existingRestaurant == null) return NotFound($"Restaurant by id: {id} was not found!");

            return Ok(existingRestaurant);
        }

        [HttpPost]
        public ActionResult<Restaurant> PostRestaurant(Restaurant restaurant)
        {
            var createdRestaurant = _service.PostRestaurant(restaurant);
            if (createdRestaurant == null) return BadRequest();

            return CreatedAtAction(nameof(GetRestaurantById), new {id = restaurant.Id}, restaurant);
        }

        [HttpPut("{id}")]
        public IActionResult PutRestaurant(int id, Restaurant restaurant)
        {
            var success = _service.PutRestaurant(id, restaurant);
            if (success == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            var success = _service.DeleteRestaurant(id);
            if(!success) return NotFound();

            return NoContent();
        }
    }
}
