using Auth_APIs.DTOs.Request;
using Auth_APIs.DTOs.Response;
using Auth_APIs.Entities;
using Auth_APIs.IRepositories;
using Auth_APIs.IService;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Auth_APIs.Service
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskRepository _taskRepository;

        public UserTaskService(IUserTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<UserTaskResponseDTO> AddTask(int userId, UserTaskRequestDTO request)
        {
            var newTask = new UserTask
            {
                Title = request.Title,
                Description = request.Description,
                UserId = userId
            };

            var addedTask = await _taskRepository.AddTask(newTask);
            return new UserTaskResponseDTO()
            {
                Id = addedTask.Id,
                Title = addedTask.Title,
                Description = addedTask.Description,
                UserId= addedTask.UserId
            };
        }

        public async Task<IEnumerable<UserTaskResponseDTO>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return tasks.Select(t => new UserTaskResponseDTO()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                UserId = t.UserId
            });
        }

        public async Task<IEnumerable<UserTaskResponseDTO>> GetTasksByUserId(int userId)
        {
            var tasks = await _taskRepository.GetTasksByUserId(userId);
            return tasks.Select(t => new UserTaskResponseDTO()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                UserId = t.UserId
            });
        }

        public async Task<UserTaskResponseDTO> GetTaskById(int taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if(task == null)
            {
                throw new Exception("Check the Task ID");
            }
            return new UserTaskResponseDTO()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                UserId = task.UserId
            };
        }

        public async Task<UserTaskResponseDTO> UpdateTask(int taskId, UserTaskRequestDTO request)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new Exception("Check the Task ID");

            task.Title = request.Title;
            task.Description = request.Description;

            var updatedTask = await _taskRepository.UpdateTask(task);
            return new UserTaskResponseDTO()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                UserId = task.UserId
            };
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            return await _taskRepository.DeleteTask(taskId);
        }
    }
}
