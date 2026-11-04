using FoodDelivery.Data;
using FoodDelivery.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers
{
    public class FoodsController : ControllerBase
    {
        private readonly FoodDeliveryDbContext _db;

        public FoodsController(FoodDeliveryDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _db.Foods.FindAsync(id);

            if(food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoodList()
        {
            return await _db.Foods.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostFood(Food food)
        {
            _db.Foods.Add(food);
            await _db.SaveChangesAsync();

            return Ok(food);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _db.Foods.FindAsync(id);
            if(food == null)
            {
                return NotFound();
            }

            _db.Foods.Remove(food);
            await _db.SaveChangesAsync();

            return NoContent();
        }


    }
}
