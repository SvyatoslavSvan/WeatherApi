using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Services;
using WeatherForecast.DAL.Context;
using WeatherForecast.DAL.Interfaces.Base;
using WeatherForecast.DAL.Repositories.Base;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.Domain.Services.Interfaces.Base;
using WeatherForecast.Domain.Services.Services;
using WeatherForecast.Domain.Services.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();
builder.Services.AddScoped<IRepository<TemperatureState>, Repository<TemperatureState>>();
builder.Services.AddScoped<IService<TemperatureState>, Service<TemperatureState>>();
builder.Services.AddScoped<ITemperatureStateService, TemperatureStateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
