using DAL.DTO.Task;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Task = DAL.Models.Task;

namespace WebApplication.Controllers;

[Route("api/tasks")]
[ApiController]
public sealed class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasksAsync()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTasksAsync([FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        var task = new Task
        {
            Name = createTaskDto.Name
        };

        var createdTask = await _taskRepository.AddTaskAsync(task);

        return CreatedAtAction(nameof(GetAllTasksAsync), new { id = task.Id }, createdTask);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        var taskExists = await _taskRepository.TaskExistsAsync(id);
        if (!taskExists)
        {
            return NotFound(new { Message = $"Task with ID {id} not found." });
        }

        try
        {
            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An error occurred while deleting the task. {ex.Message}" });
        }
    }

}