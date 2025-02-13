using Auth_APIs.Database;
using Auth_APIs.Entities;
using Auth_APIs.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Auth_APIs.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly AppDbContext _dbContext;

        public UserTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserTask> AddTask(UserTask task)
        {
            await _dbContext.UserTasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<UserTask>> GetAllTasks()
        {
            return await _dbContext.UserTasks.Include(t => t.User).ToListAsync();
        }

        public async Task<IEnumerable<UserTask>> GetTasksByUserId(int userId)
        {
            return await _dbContext.UserTasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<UserTask> GetTaskById(int taskId)
        {
            return await _dbContext.UserTasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<UserTask> UpdateTask(UserTask task)
        {
            _dbContext.UserTasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await _dbContext.UserTasks.FindAsync(taskId);
            if (task == null)
                return false;

            _dbContext.UserTasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

