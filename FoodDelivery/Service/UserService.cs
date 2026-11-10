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

        public IEnumerable<ResponseUserDTO> GetUserDTOList()
        {
            var usersList = _repository.GetAll();

            var usersDTOList = new List<ResponseUserDTO>();
            foreach (var user in usersList)
            {
                var userDto = CreateUserDTO(user);
                usersDTOList.Add(userDto);
            }

            return usersDTOList;
        }

        public ResponseUserDTO? GetUserDTOById(int id)
        {
            var user = _repository.GetById(id);
            if (user == null) return null;
            var userDto = CreateUserDTO(user);
            return userDto;
        }

        public ResponseUserDTO? PostUser(RequestUserDTO requestUser)
        {
            //Todo checks and returns of correct type for status codes
            if (requestUser == null) return null;
            if (requestUser.Id != 0) return null;
            if (CheckUsernameAvailability(requestUser)) return null;

            var user = MappingUser(requestUser);

            _repository.Add(user);
            var userDto = CreateUserDTO(user);

            return userDto;
        }

        public User? PutUser(int id, RequestUserDTO requestUser)
        {
            var existingUser = _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingUser == null || id != requestUser.Id) return null;
            if (CheckUsernameAvailability(requestUser)) return null;

            var user = MappingUser(requestUser);

            _repository.Update(user);
            return user;
        }

        public bool DeleteUserint(int id)
        {
            var existingUser = _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingUser == null) return false;

            _repository.Delete(existingUser);
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

        private User MappingUser(RequestUserDTO requestUser)
        {
            User user = new User
            {
                Id = requestUser.Id,
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
        private bool CheckUsernameAvailability(RequestUserDTO updateUser)
        {
            return _repository.CheckUsername(updateUser.Username);
        }
    }
}
