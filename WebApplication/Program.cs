using DAL;
using Microsoft.EntityFrameworkCore;

namespace WebApplication;

using Microsoft.AspNetCore.Builder;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        builder.Services.AddRepositories();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        // app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.Run();
    }
}