namespace WeatherApi.Models;

public class WeatherReponse
{
    public string City { get; set; }
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public string Description { get; set; }
}