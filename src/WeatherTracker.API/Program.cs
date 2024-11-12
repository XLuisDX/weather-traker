using WeatherTracker.Core.Interfaces.Services;
using WeatherTracker.Infrastructure.External.WeatherService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.Configure<WeatherServiceConfig>(
    builder.Configuration.GetSection("WeatherService"));

builder.Services.AddHttpClient<IWeatherService, OpenWeatherMapService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IWeatherDataCacheService, WeatherDataCacheService>();

// CORS policy for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();