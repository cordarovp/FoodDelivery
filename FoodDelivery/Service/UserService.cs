using FoodDelivery.Entity;
using FoodDelivery.Repository;

namespace FoodDelivery.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserDTO> GetUserDTOList()
        {
            var usersList = _repository.GetUserList();

            var usersDTOList = new List<UserDTO>();
            foreach (var user in usersList)
            { 
                var userDto = CreateUserDTO(user);
                usersDTOList.Add(userDto);
            }

            return usersDTOList;
        }

        public UserDTO? GetUserDTOById(int id)
        {
            var user = _repository.GetUserId(id);
            if (user == null) return null;
            var userDto = CreateUserDTO(user);
            return userDto;
        }

        public User? GetUserById(int id)
        {
            return _repository.GetUserId(id);
        }

        public User? PostUser(User user)
        {
            //Todo checks and returns of correct type for status codes
            if (!CheckUsernameAvailability(user)) return null;

            _repository.AddUser(user);
            return user;
        }

        public User? PutUser(int id, User user)
        {
            var existingUser = _repository.GetUserId(id);
            //Todo checks and returns of correct type for status codes
            if (existingUser == null || id != user.Id) return null;
            if (!CheckUsernameAvailability(user)) return null;

            _repository.UpdateUser(user);
            return user;
        }

        public bool DeleteUserint(int id)
        {
            var existingUser = _repository.GetUserId(id);
            //Todo checks and returns of correct type for status codes
            if (existingUser == null) return false;

            _repository.RemoveUser(existingUser);
            return true;
        }

        private UserDTO CreateUserDTO(User existingUser)
        {
            var userDto = new UserDTO
            {
                Id = existingUser.Id,
                FullName = existingUser.FirstName + " " + existingUser.LastName,
                Username = existingUser.Username,
                GeoLat = existingUser.GeoLat,
                GeoLon = existingUser.GeoLon
            };

            return userDto;
        }

        //This method will return true if the forwarded username is not being used.
        private bool CheckUsernameAvailability(User updateUser)
        {
            var usersList = _repository.GetUserList();

            foreach (var existingUser in usersList)
            {
                if (existingUser.Username == updateUser.Username)
                {
                    if(existingUser.Id == updateUser.Id)
                        return true;
                    else
                        return false;
                }else continue;
            }
            return true;
        }
    }
}
