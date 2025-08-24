using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherservice)
    {
        _weatherService = weatherservice;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        try
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error fetching weather data: {ex.Message}");
        }
    }

    [HttpGet("forecast/lat={lat}/lon={lon}")]
    public async Task<IActionResult> GetForecast(double lat, double lon)
    {
        try
        {
            var forecast = await _weatherService.GetForecastAsync(lat, lon);
            return Ok(forecast);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error fetching forecast data: {ex.Message}");
        }
    }
}