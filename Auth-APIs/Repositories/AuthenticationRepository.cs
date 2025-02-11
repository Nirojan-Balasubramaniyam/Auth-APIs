using Auth_APIs.Database;
using Auth_APIs.Entities;
using Auth_APIs.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Auth_APIs.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _dbContext;
        public AuthenticationRepository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<User> AddUser(User user) 
        {
            await _dbContext.Users.AddAsync(user); 
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;

        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userList = await _dbContext.Users.ToListAsync();
            return userList;
        }
        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user;
        }
    }
}
