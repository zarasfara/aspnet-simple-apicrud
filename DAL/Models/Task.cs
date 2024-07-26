using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

[Table("tasks")]
public sealed class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Int64 Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}