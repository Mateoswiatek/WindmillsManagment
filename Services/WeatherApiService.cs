using System.Net;
using Newtonsoft.Json;
using MgWindManager.Models;
using MgWindManager.Services.Interfaces;

namespace MgWindManager.Services;

public class WeatherApiService : IWeatherApiService
{
    private const string API_KEY = "5af0b02c6d323fdacc4c6daac0468a11";
    
    public WeatherResponse GetData(string lat, string lon)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API_KEY}&units=metric&lang=pl";

        var web = new WebClient();
        var response = web.DownloadData(url);
        string json = System.Text.Encoding.Default.GetString(response);
        var myDeserializedClass = JsonConvert.DeserializeObject<WeatherResponse>(json);

        // myDeserializedClass.main
        
        return myDeserializedClass;
    }
}