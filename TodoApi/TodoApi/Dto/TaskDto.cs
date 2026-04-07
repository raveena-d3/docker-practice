using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class TaskDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Task title cannot be empty")]
    public string Title { get; set; } = string.Empty;
}