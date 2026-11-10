using FoodDelivery.Entity;
using FoodDelivery.Service;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponseUserDTO>> GetUsers()
        {
            var usersList = _service.GetUserDTOList();
            return Ok(usersList);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseUserDTO> GetUserDTOById(int id)
        {
            var existingUser = _service.GetUserDTOById(id);
            if (existingUser == null) return NotFound($"User with id: {id} was not found!");

            return Ok(existingUser);
        }

        [HttpPost]
        public ActionResult<ResponseUserDTO> PostUser(RequestUserDTO user)
        {
            var createdUser = _service.PostUser(user);
            if (createdUser == null) return BadRequest();

            return CreatedAtAction(nameof(GetUserDTOById), new { id = user.Id }, createdUser);
        }

        [HttpPut("{id}")]
        //Todo autorizacija
        public IActionResult PutUser(int id, RequestUserDTO user)
        {
            var success = _service.PutUser(id, user);
            if(success == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        //Todo autorizacija
        public IActionResult DeleteUser(int id)
        {
            var success = _service.DeleteUserint(id);

            if(!success) return NotFound();

            return NoContent();
        }
    }
}
