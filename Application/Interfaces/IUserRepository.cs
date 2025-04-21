using Syfora_Test.Domain;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(Guid id);
        Task<User> AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User?> GetUserByLoginAsync(string login);
    }
}
