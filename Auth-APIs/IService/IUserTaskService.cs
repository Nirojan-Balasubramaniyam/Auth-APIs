using Auth_APIs.DTOs.Request;
using Auth_APIs.DTOs.Response;

namespace Auth_APIs.IService
{
    public interface IUserTaskService
    {
        Task<UserTaskResponseDTO> AddTask(int userId, UserTaskRequestDTO request);
        Task<IEnumerable<UserTaskResponseDTO>> GetAllTasks();
        Task<IEnumerable<UserTaskResponseDTO>> GetTasksByUserId(int userId);
        Task<UserTaskResponseDTO> GetTaskById(int taskId);
        Task<UserTaskResponseDTO> UpdateTask(int taskId, UserTaskRequestDTO request);
        Task<bool> DeleteTask(int taskId);
    }
}
