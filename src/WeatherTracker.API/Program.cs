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
        policy.WithOrigins(
               "https://weather-tracker-frontend.azurewebsites.net", 
                "http://weather-tracker-frontend.azurewebsites.net",
                "https://localhost:3000",
                "http://localhost:3000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition");
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();