using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2.Infrastructure.Context;
using Project2.Infrastructure.Mapper;
using Project2.Infrastructure.Repos;
using Project2.Infrastructure.Repos.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IFlowerRepo, FlowerRepo>();
builder.Services.AddScoped<IGardenRepo, GardenRepo>();
builder.Services.AddScoped<IOwnerRepo, OwnerRepo>();
builder.Services.AddScoped<ITreeRepo, TreeRepo>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GardenContext>(c =>
{
    c.UseSqlite($@"Data Source=Garden.db")
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        using (var db = scope.ServiceProvider.GetService<GardenContext>())
        {
            if (db is null)
                throw new Exception("No DB!");

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            await db.SeedBogusAsync();
        }
    }
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
