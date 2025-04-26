using Application.Interfaces;
using Application.Models;
using Syfora_Test.Domain;
using Syfora_Test.Models;

namespace Application.DataProviders
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDtoOut>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => Map(u)).ToList();
        }

        public async Task<UserDtoOut?> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserAsync(id);
            return user == null ? null : Map(user);
        }

        public async Task<bool> IsLoginExist(string login)
        {
            var user = await _userRepository.GetUserByLoginAsync(login);
            return user != null;
        }

        public async Task<UserDtoOut> AddUserAsync(UserDtoIn userDto)
        {
            var user = MapUserDtoToUser(userDto);
            await _userRepository.AddUserAsync(user);
            return Map(user);
        }

        public async Task UpdateUserAsync(Guid id, UserDtoIn userDto)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null) throw new KeyNotFoundException("User not found");

            user.Login = userDto.Login;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null) throw new KeyNotFoundException("User not found");

            await _userRepository.DeleteUserAsync(user);
        }

        private UserDtoOut Map(User user)
        {
            return new UserDtoOut
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        private User MapUserDtoToUser(UserDtoIn userDto)
        {
            return new User
            {
                Login = userDto.Login,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };
        }
    }
}