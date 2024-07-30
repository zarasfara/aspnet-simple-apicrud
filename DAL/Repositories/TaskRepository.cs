using DAL.DTO.Task;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = DAL.Models.Task;

namespace DAL.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _dbContext;

    public TaskRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Task>> GetAllTasksAsync()
    {
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task<Task> AddTaskAsync(Task task)
    {
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    public async System.Threading.Tasks.Task UpdateTaskAsync(long id, UpdateTaskDto data)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task == null)
        {
            throw new KeyNotFoundException($"Task with id {id} not found.");
        }

        task.Name = data.Name;

        _dbContext.Tasks.Update(task);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("An error occurred while updating the task in the database.", ex);
        }
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(long id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);
        if (task != null)
        {
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> TaskExistsAsync(long id)
    {
        return await _dbContext.Tasks.AnyAsync(t => t.Id == id);
    }
    
    public async Task<Task> FindByIdAsync(long id)
    {
        var task = await _dbContext.Tasks.FindAsync(id);

        if (task == null)
        {
            throw new KeyNotFoundException($"Task with id {id} not found.");
        }

        return task;
    }
}