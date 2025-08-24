namespace WeatherApi.Models;

public class ForecastResponse 
{
	public string DateTime { get; set; }
	public double Temperature { get; set; }
	public int Humidity { get; set; }
	public double TemperatureMin { get; set; }
	public double TemperatureMax { get; set; }
}