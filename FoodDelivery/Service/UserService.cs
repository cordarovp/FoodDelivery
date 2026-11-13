using FoodDelivery.DTO;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResponseUserDTO>> GetUserDTOList()
        {
            var usersList = await _repository.GetAll();

            var usersDTOList = new List<ResponseUserDTO>();
            foreach (var user in usersList)
            {
                var userDto = CreateUserDTO(user);
                usersDTOList.Add(userDto);
            }

            return usersDTOList;
        }

        public async Task<ResponseUserDTO?> GetUserDTOById(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null) return null;
            var userDto = CreateUserDTO(user);
            return userDto;
        }

        public async Task<ResponseUserDTO?> PostUser(RequestUserDTO requestUser)
        {
            //Todo checks and returns of correct type for status codes
            if (requestUser == null) return null;
            if (await CheckUsernameAvailability(requestUser)) return null;

            var user = MappingUser(0, requestUser);

            await _repository.Add(user);
            var userDto = CreateUserDTO(user);

            return userDto;
        }

        public async Task<ResponseUserDTO?> PutUser(int id, RequestUserDTO requestUser)
        {
            var existingUser = await _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingUser == null) return null;
            if (await CheckUsernameAvailability(requestUser)) return null;

            var user = MappingUser(id, requestUser);
            await _repository.Update(user);

            var responseUser = CreateUserDTO(user);
            return responseUser;
        }

        public async Task<bool> DeleteUserint(int id)
        {
            var existingUser = await _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingUser == null) return false;

            await _repository.Delete(existingUser);
            return true;
        }


        private ResponseUserDTO CreateUserDTO(User existingUser)
        {
            var userDto = new ResponseUserDTO
            {
                Id = existingUser.Id,
                FullName = existingUser.FirstName + " " + existingUser.LastName,
                Username = existingUser.Username,
                GeoLat = existingUser.GeoLat,
                GeoLon = existingUser.GeoLon
            };

            return userDto;
        }

        //Todo IsAdmin set and autharization
        private User MappingUser(int id, RequestUserDTO requestUser)
        {
            User user = new User
            {
                Id = id,
                FirstName = requestUser.FirstName,
                LastName = requestUser.LastName,
                GeoLat = requestUser.GeoLat,
                GeoLon = requestUser.GeoLon,
                Username = requestUser.Username,
                Password = requestUser.Password,
                IsAdmin = false
            };

            return user;
        }

        //This method will return true if the forwarded username IS being used.
        private async Task<bool> CheckUsernameAvailability(RequestUserDTO updateUser)
        {
            return await _repository.CheckUsername(updateUser.Username);
        }
    }
}
