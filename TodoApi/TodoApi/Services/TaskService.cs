using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    // Get all tasks
    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Get task by ID
    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    // Business logic: trim + validate title, then create
    public async Task<TaskItem> CreateTaskAsync(string title)
    {
        var task = new TaskItem
        {
            Title = title.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(task);
    }

    // Business logic: trim + validate title, then update
    public async Task<TaskItem?> UpdateTaskAsync(int id, string title)
    {
        return await _repository.UpdateAsync(id, title.Trim());
    }

    // Delete task
    public async Task<TaskItem?> DeleteTaskAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}