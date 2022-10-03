using System;
using System.Net;
using System.Text;
using System.Device.Location;
using RestSharp;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter city");
        string city = Console.ReadLine();        

        string apiKey = "1e0dcb1b35c33c85c8f53ba10ee78100";       
        OpenWeatherService openWeatherService = new OpenWeatherService();
        var CurrentWeather = await openWeatherService.GetCurrentWeather(city, apiKey);

        Console.WriteLine($"The weather of {city}:");
        Console.WriteLine($"main: {CurrentWeather.weather[0].main}");
        Console.WriteLine($"description: {CurrentWeather.weather[0].description}");
        Console.WriteLine($"pressure: {CurrentWeather.main.pressure}");
        Console.WriteLine($"temperature: {CurrentWeather.main.temp}");
        Console.WriteLine($"wind speed: {CurrentWeather.wind.speed}");
    }

}

public class OpenWeatherService 
{
    public async Task<CurrentWeatherDTO> GetCurrentWeather(string city, string apiKey)
    {
        CurrentWeatherDTO weatherForecast = new();
        string requestUri = GetCurrentWeatherRequestUri(city, apiKey);
        HttpClient client = new();

        HttpResponseMessage response = await client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            weatherForecast = await response.Content.ReadAsAsync<CurrentWeatherDTO>();

        return weatherForecast;
    }

    private string GetCurrentWeatherRequestUri(string city, string apiKey) =>
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";


}   
    
    public class CurrentWeatherDTO
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }       
        public MainW main { get; set; }        
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }        
        public string name { get; set; }       
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class MainW
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }


    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }
//checked
