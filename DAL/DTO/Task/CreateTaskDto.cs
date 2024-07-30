using System.ComponentModel.DataAnnotations;

namespace DAL.DTO.Task;

public sealed record CreateTaskDto
{
    [Required] public string Name { get; init; }
}