using FoodDelivery.DTO.User;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Result;
using FoodDelivery.Result.Error;
using FoodDelivery.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _service;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public AuthService(IUserService service, IUserRepository repository, IConfiguration config)
        {
            _service = service;
            _repository = repository;
            _config = config;
        }

        public async Task<Reply<string?>> Login(LoginUserDTO loginUser)
        {
            var user = await _repository.FindUserWithUsername(loginUser.Username);
            if(user == null || user.Password != loginUser.Password)
            {
                return Reply<string?>.Failure(ErrorCore.Unauthorized("Incorrect username or password"));
            }

            return Reply<string?>.Success(GenerateJwtToken(user));
        }

        public async Task<Reply<ResponseUserDTO>> Register(RequestUserDTO registerUser)
        {
           return await _service.PostUser(registerUser);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtTestSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtTestSettings:Issuer"],
                audience: _config["JwtTestSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
