using DAL.DTO.Task;

namespace DAL.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<Models.Task>> GetAllTasksAsync();

    Task<Models.Task> AddTaskAsync(Models.Task task);

    Task UpdateTaskAsync(long id, UpdateTaskDto data);

    Task DeleteTaskAsync(long id);

    Task<bool> TaskExistsAsync(long id);

    Task<Models.Task> FindByIdAsync(long id);
}
