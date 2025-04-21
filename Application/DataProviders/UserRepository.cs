using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Syfora_Test.Domain;

namespace Application.DataProviders
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
             await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
