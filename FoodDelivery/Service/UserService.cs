using FoodDelivery.DTO.User;
using FoodDelivery.Entity;
using FoodDelivery.Reply;
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

        public async Task<Reply<ResponseUserDTO>> GetUserDTOById(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null) return Reply<ResponseUserDTO>.NotFoundError($"User with id: '{id}' was not found!");
            var userDto = CreateUserDTO(user);
            return Reply<ResponseUserDTO>.Success(userDto);
        }

        public async Task<Reply<ResponseUserDTO>> PostUser(RequestUserDTO requestUser)
        {
            if (requestUser == null) return Reply<ResponseUserDTO>.ValidationError("User was not forwarded!");
            if (await IsUsernameTakenAsync(0, requestUser)) return Reply<ResponseUserDTO>.ValidationError($"User with username: '{requestUser.Username}' already exists!");

            var user = MappingUser(0, requestUser);
            await _repository.Add(user);

            var userDto = CreateUserDTO(user);
            return Reply<ResponseUserDTO>.Success(userDto);
        }

        public async Task<Reply<bool>> PutUser(int id, RequestUserDTO requestUser)
        {
            var existingUser = await _repository.GetById(id);

            if (existingUser == null) return Reply<bool>.NotFoundError($"User with id: {id} was not found!");
            if (await IsUsernameTakenAsync(id, requestUser)) return Reply<bool>.ValidationError($"User with username: '{requestUser.Username}' already exists!");

            var user = MappingUser(id, requestUser);
            await _repository.Update(user);
            return Reply<bool>.Success(true);
        }

        public async Task<Reply<bool>> DeleteUser(int id)
        {
            var existingUser = await _repository.GetById(id);
            if (existingUser == null) return Reply<bool>.NotFoundError($"Food with id: {id} was not found!");

            await _repository.Delete(existingUser);
            return Reply<bool>.Success(true);
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

        private async Task<bool> IsUsernameTakenAsync(int id, RequestUserDTO updateUser)
        {
            var user =  await _repository.CheckUsername(updateUser.Username);
            if (user == null) return false;
            if (user.Id.Equals(id)) return false;
            else return true;
        }
    }
}
