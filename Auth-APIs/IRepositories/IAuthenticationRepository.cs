using Auth_APIs.Entities;

namespace Auth_APIs.IRepositories
{
    public interface IAuthenticationRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
    }
}
