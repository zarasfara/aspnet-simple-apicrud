using System.Globalization;
using DAL.DTO.Task;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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

        return CreatedAtAction(nameof(FindByIdAsync), new {id = task.Id} , createdTask);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        if (!await _taskRepository.TaskExistsAsync(id))
        {
            return NotFound(new { Message = $"Task with ID {id} not found." });
        }

        try
        {
            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An error occurred while deleting the task. {e.Message}" });
        }
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> UpdateTask(long id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        try
        {
            await _taskRepository.UpdateTaskAsync(id, updateTaskDto);
            return Ok("Task updated successfully.");
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An unexpected error occurred: {e.Message}" });
        }
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindByIdAsync(long id)
    {
        try
        {
            var task = await _taskRepository.FindByIdAsync(id);

            return Ok(task);
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new {e.Message});
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"An unexpected error occurred: {e.Message}" });
        }
    }
}