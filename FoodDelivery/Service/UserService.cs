using FoodDelivery.DTO.User;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Result;
using FoodDelivery.Result.Error;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            if (user == null) return Reply<ResponseUserDTO>.Failure(ErrorCore.NotFound($"User with id: '{id}' was not found!"));
            var userDto = CreateUserDTO(user);
            return Reply<ResponseUserDTO>.Success(userDto);
        }

        public async Task<Reply<ResponseUserDTO>> PostUser(RequestUserDTO requestUser)
        {
            if (requestUser == null) return Reply<ResponseUserDTO>.Failure(ErrorCore.Validation("User was not forwarded!"));
            if (await IsUsernameTakenAsync(0, requestUser)) return Reply<ResponseUserDTO>.Failure(ErrorCore.Conflict($"User with username: '{requestUser.Username}' already exists!"));

            var user = MappingUser(0, requestUser);
            await _repository.Add(user);

            var userDto = CreateUserDTO(user);
            return Reply<ResponseUserDTO>.Success(userDto);
        }

        public async Task<Reply> PutUser(int id, RequestUserDTO requestUser)
        {
            var existingUser = await _repository.GetById(id);

            if (existingUser == null) return Reply.Failure(ErrorCore.NotFound($"User with id: {id} was not found!"));
            if (await IsUsernameTakenAsync(id, requestUser)) return Reply.Failure(ErrorCore.Conflict($"User with username: '{requestUser.Username}' already exists!"));

            var user = MappingUser(id, requestUser, existingUser.IsAdmin);
            try
            {
                await _repository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"User with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        public async Task<Reply> DeleteUser(int id)
        {
            var existingUser = await _repository.GetById(id);
            if (existingUser == null) return Reply.Failure(ErrorCore.NotFound($"User with id: {id} was not found!"));

            try
            {
                await _repository.Delete(existingUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"User with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
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
        private User MappingUser(int id, RequestUserDTO requestUser, bool isAdmin = false)
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
                IsAdmin = isAdmin
            };

            return user;
        }

        private async Task<bool> IsUsernameTakenAsync(int id, RequestUserDTO updateUser)
        {
            var user = await _repository.FindUserWithUsername(updateUser.Username);
            if (user == null) return false;
            if (user.Id.Equals(id)) return false;
            else return true;
        }
    }
}
