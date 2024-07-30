using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.DTO.Task;

public record UpdateTaskDto
{
    [Required]
    public string Name { get; init; }
}