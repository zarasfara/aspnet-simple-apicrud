using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class TaskRepository(AppDbContext dbContext) : ITaskRepository
{
    public async Task<IEnumerable<Models.Task>> GetAllTasksAsync()
    {
        return await dbContext.Tasks.ToListAsync();
    }

    public Task<Task> GetTaskByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Task> AddTaskAsync(Models.Task task)
    {
        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();
        return task;
    }

    public Task UpdateTaskAsync(Task task)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTaskAsync(long id)
    {
        var task = await dbContext.Tasks.FindAsync(id);
        if (task != null)
        {
            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> TaskExistsAsync(long id)
    {
        return await dbContext.Tasks.AnyAsync(t => t.Id == id);
    }
}