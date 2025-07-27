using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
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
    builder.Services.AddScoped<IRefreshTokenRepository, RestApiTemplate.Repositories.Mongo.RefreshTokenRepository>();
}
else if (dbType == "postgres")
{
    builder.Services.AddDbContext<PostgresDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
    builder.Services.AddScoped<IUserRepository, RestApiTemplate.Repositories.Postgres.UserRepository>();
    builder.Services.AddScoped<IRefreshTokenRepository, RestApiTemplate.Repositories.Postgres.RefreshTokenRepository>();
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme =
    x.DefaultChallengeScheme =
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y =>
{
    y.SaveToken = false;
    y.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

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

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
