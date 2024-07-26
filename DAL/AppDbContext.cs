using Microsoft.EntityFrameworkCore;
using Task = DAL.Models.Task;

namespace DAL;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Task> Tasks { get; init; }
}