using Microsoft.EntityFrameworkCore;
using RestApiTemplate.Database;
using RestApiTemplate.Database.Postgres;
using RestApiTemplate.Middlewares;
using RestApiTemplate.Repositories;
using RestApiTemplate.Services;
using RestApiTemplate.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var dbType = builder.Configuration["DatabaseType"]; // e.g., "postgres" or "mongo"

if (dbType == "mongo")
{
    builder.Services.AddSingleton<MongoDbContext>();
    builder.Services.AddScoped<IUserRepository, RestApiTemplate.Repositories.Mongo.UserRepository>();
}
else if (dbType == "postgres")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
    builder.Services.AddScoped<IUserRepository, RestApiTemplate.Repositories.Postgres.UserRepository>();
}

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
