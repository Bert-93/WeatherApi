using Microsoft.OpenApi.Models;
using DotNetEnv;

// Carga variables de entorno desde el archivo .env
//DotNetEnv.Env.Load();
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var ApiKey = Environment.GetEnvironmentVariable("API_KEY");
var BaseUrl = Environment.GetEnvironmentVariable("BASE_URL");

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Weather API",
        Version = "v1",
        Description = "Simple API Gateway for OpenWeatherMap"
    });
});
builder.Services.AddHttpClient<WeatherApi.Services.WeatherService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API v1");
        c.RoutePrefix = string.Empty;
    });
}

//app.UseHttpsRedirection();
app.MapControllers(); 

app.Run();