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
        var url = $"{_baseUrl}/weather?q={city name}&appid={_apiKey}&units=metric&lang=es";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error fetching weather data: {response.ReasonPhrase}");
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
}