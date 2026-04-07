using TodoApi.Models;

namespace TodoApi.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<TaskItem> CreateTaskAsync(string title);
    Task<TaskItem?> UpdateTaskAsync(int id, string title);
    Task<TaskItem?> DeleteTaskAsync(int id);
}