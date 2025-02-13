using Auth_APIs.Entities;

namespace Auth_APIs.IRepositories
{
    public interface IUserTaskRepository
    {
        Task<UserTask> AddTask(UserTask task);
        Task<IEnumerable<UserTask>> GetAllTasks();
        Task<IEnumerable<UserTask>> GetTasksByUserId(int userId);
        Task<UserTask> GetTaskById(int taskId);
        Task<UserTask> UpdateTask(UserTask task);
        Task<bool> DeleteTask(int taskId);
    }
}
