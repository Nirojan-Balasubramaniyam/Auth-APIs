using Auth_APIs.DTOs.Request;
using Auth_APIs.IService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly IUserTaskService _taskService;

        public UserTaskController(IUserTaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpPost]
        public async Task<IActionResult> AddTask(UserTaskRequestDTO request)
        {
            try
            {
                var newTask = await _taskService.AddTask(request.UserId, request);
                return Ok(new { message = "Task added successfully", newTask });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while adding the task", error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while retrieving tasks", error = ex.Message });
            }
        }

        [HttpGet("my-tasks")]
        public async Task<IActionResult> GetUserTasks()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var tasks = await _taskService.GetTasksByUserId(userId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while retrieving user tasks", error = ex.Message });
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            try
            {
                var task = await _taskService.GetTaskById(taskId);
                if (task == null)
                    return NotFound("Task not found");
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while retrieving the task", error = ex.Message });
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, UserTaskRequestDTO request)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTask(taskId, request);
                if (updatedTask == null)
                    return NotFound("Task not found");
                return Ok(new { message = "Task updated successfully", updatedTask });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while updating the task", error = ex.Message });
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                var success = await _taskService.DeleteTask(taskId);
                if (!success)
                    return NotFound("Task not found");
                return Ok(new { message = "Task deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while deleting the task", error = ex.Message });
            }
        }

    }
}
