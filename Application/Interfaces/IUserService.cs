using Syfora_Test.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserAsync(Guid id);
        Task<bool> IsLoginExist(string login);
        Task<UserDto> AddUserAsync(UserDto user);
        Task UpdateUserAsync(UserDto user);
        Task DeleteUserAsync(Guid id);
    }
}
