# WeatherApi

Simple weather API that provides current weather data for any location, using the external API OpenWeatherMap. 
It supports multiple languages and units of measurement.
Was made with C# and uses the OpenWeatherMap API to fetch weather data.

# Goal:
 - Provide a simple and easy-to-use weather API
 - Refresh concepts that I learned in C# time ago
 - Have an easy API to review and use in the future the structure

# Endpoints:

/api/weather/current/{city} → Devuelve clima actual de la ciudad.

/api/weather/forecast/{city} → Devuelve pronóstico de los próximos 5 días.

/api/weather/temperature/{city} → Devuelve solo la temperatura actual.

# To improve:

 - Caching responses to avoid excessive usage of the external API.
 - Adding query logs.
 - Creating a DTO layer to return clean data to the client.