using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using WeatherApi.Models;

namespace WeatherApi.Services;

public class  WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeather:ApiKey"];
        _baseUrl = configuration["OpenWeather:BaseUrl"];
    }

    public async Task<WeatherReponse> GetWeatherAsync (string city)
    {
        var url = $"{_baseUrl}/weather?q={city}&appid={_apiKey}&units=metric&lang=es";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error consultando API: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var root = doc.RootElement;

        return new WeatherReponse
        {
            City = root.GetProperty("name").GetString(),
            Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
            Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
            Description = root.GetProperty("weather")[0].GetProperty("description").GetString()
        };
    }

    public async Task<ForecastResponse> GetForecastAsync(double lat, double lon)
    {
        var url = $"{_baseUrl}/forecast?lat={lat}&lon={lon}&appid={_apiKey}&units=metric&lang=es";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error consultando API: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var list = root.GetProperty("list")[0];
        var main = list.GetProperty("main");
        var dateTime = list.GetProperty("dt_txt").GetString();

        return new ForecastResponse
        {
            DateTime = dateTime,
            Temperature = main.GetProperty("temp").GetDouble(),
            Humidity = main.GetProperty("humidity").GetInt32(),
            TemperatureMin = main.GetProperty("temp_min").GetDouble(),
            TemperatureMax = main.GetProperty("temp_max").GetDouble()
        };
    }
}