using FoodDelivery.DTO.User;
using FoodDelivery.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RequestUserDTO requestUser)
        {
            var registeredUser = await _authService.Register(requestUser);
            return this.ToActionResult(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            var token = await _authService.Login(loginUser);
            return this.ToActionResult(token);
        }
    }
}
