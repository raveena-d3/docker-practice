using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]         // Route: /tasks
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    // GET /tasks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _service.GetAllTasksAsync();
        return Ok(tasks);
    }

    // GET /tasks/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _service.GetTaskByIdAsync(id);

        if (task == null)
            return NotFound(new { error = "Task not found" });

        return Ok(task);
    }

    // POST /tasks
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateTaskAsync(dto.Title);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateTaskAsync(id, dto.Title);

        if (updated == null)
            return NotFound(new { error = "Task not found" });

        return Ok(updated);
    }

    // DELETE /tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteTaskAsync(id);

        if (deleted == null)
            return NotFound(new { error = "Task not found" });

        return Ok(new { message = "Task deleted", task = deleted });
    }
}