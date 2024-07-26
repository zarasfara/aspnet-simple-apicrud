namespace DAL.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<Models.Task>> GetAllTasksAsync();

    // Task<Models.Task> GetTaskByIdAsync(int id);

    Task<Models.Task> AddTaskAsync(Models.Task task);

    // Task UpdateTaskAsync(Models.Task task);

    Task DeleteTaskAsync(long id);

    Task<bool> TaskExistsAsync(long id);
}
