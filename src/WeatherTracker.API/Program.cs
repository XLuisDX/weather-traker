using WeatherTracker.Core.Interfaces.Services;
using WeatherTracker.Infrastructure.External.WeatherService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WeatherServiceConfig>(
    builder.Configuration.GetSection("WeatherService"));

builder.Services.AddHttpClient<IWeatherService, OpenWeatherMapService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IWeatherDataCacheService, WeatherDataCacheService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet("/", () => "Weather Tracker API is running!");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();