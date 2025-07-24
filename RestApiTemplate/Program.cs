using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestApiTemplate.Database;
using RestApiTemplate.Middlewares;
using RestApiTemplate.Repositories;
using RestApiTemplate.Services;
using RestApiTemplate.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var dbType = builder.Configuration["DatabaseType"]; // e.g., "postgres" or "mongo"

if (dbType == "mongo")
{
    builder.Services.AddSingleton<MongoDbContext>();
    builder.Services.AddScoped<IUserRepository, RestApiTemplate.Repositories.Mongo.UserRepository>();
}
else if (dbType == "postgres")
{
    builder.Services.AddDbContext<PostgresDbContext>(options =>
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

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme =
    x.DefaultChallengeScheme =
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y =>
{
    y.SaveToken = false;
    y.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JWTSecret"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
