using FoodDelivery.DTO.User;
using FoodDelivery.Service.Interfaces;
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
        public async Task<ActionResult<IEnumerable<ResponseUserDTO>>> GetUsers()
        {
            var usersList = await _service.GetUserDTOList();
            return Ok(usersList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseUserDTO>> GetUserDTOById(int id)
        {
            var existingUser = await _service.GetUserDTOById(id);

            if (existingUser.IsFailure)
                return StatusCode(existingUser.Error!.StatusCode, existingUser.Error);

            return Ok(existingUser.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseUserDTO>> PostUser(RequestUserDTO user)
        {
            var createdUser = await _service.PostUser(user);
            if (createdUser.IsFailure)
                return StatusCode(createdUser.Error!.StatusCode, createdUser.Error);

            return CreatedAtAction(nameof(GetUserDTOById), new { id = createdUser.Value!.Id }, createdUser.Value);
        }

        [HttpPut("{id}")]
        //Todo autorizacija
        public async Task<IActionResult> PutUser(int id, RequestUserDTO user)
        {
            var success = await _service.PutUser(id, user);
            if (success.IsFailure)
                return StatusCode(success.Error!.StatusCode, success.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        //Todo autorizacija
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _service.DeleteUser(id);
            if (success.IsFailure)
                return StatusCode(success.Error!.StatusCode, success.Error);

            return NoContent();
        }
    }
}
