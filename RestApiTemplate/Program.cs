using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Data.Postgres;
using RestApiTemplate.Middlewares;
using RestApiTemplate.Repositories;
using RestApiTemplate.Repositories.Postgres;
using RestApiTemplate.Services;
using RestApiTemplate.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Choose PostgreSQL as the default database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Use PostgreSQL repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
