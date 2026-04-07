using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _db;

    public TaskRepository(AppDbContext db)
    {
        _db = db;
    }

    // Fetch all tasks ordered by creation date
    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _db.Tasks
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
    }

    // Fetch a single task by ID
    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _db.Tasks.FindAsync(id);
    }

    // Insert a new task into the database
    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();   // Persists to PostgreSQL
        return task;
    }

    // Update title of an existing task
    public async Task<TaskItem?> UpdateAsync(int id, string title)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task == null) return null;

        task.Title = title;
        task.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();   // Persists to PostgreSQL
        return task;
    }

    // Delete a task from the database
    public async Task<TaskItem?> DeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);

        if (task == null) return null;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();   // Persists to PostgreSQL
        return task;
    }
}