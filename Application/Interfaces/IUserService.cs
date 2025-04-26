using Application.Models;
using Syfora_Test.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDtoOut>> GetAllUsersAsync();
        Task<UserDtoOut?> GetUserAsync(Guid id);
        Task<bool> IsLoginExist(string login);
        Task<UserDtoOut> AddUserAsync(UserDtoIn user);
        Task UpdateUserAsync(Guid id, UserDtoIn user);
        Task DeleteUserAsync(Guid id);
    }
}
