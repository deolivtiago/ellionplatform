using Microsoft.EntityFrameworkCore;
using EllionPlatform.CoreAPI.Data;
using EllionPlatform.CoreAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<DataContext>(
        b => b
            .UseNpgsql(
                builder
                    .Configuration
                    .GetConnectionString("DefaultConnection")
            )
    );

builder.Services
    .AddAuthorization()
    .AddAuthentication();

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<DataContext>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("get-weather-forecast")
    .WithOpenApi()
    .RequireAuthorization();

app.MapGet("/users", async (DataContext db) => await db.Users.ToListAsync())
    .WithName("get-users")
    .WithOpenApi()
    .RequireAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
