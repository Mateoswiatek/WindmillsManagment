using MgWindManager.Models;

namespace MgWindManager.Services.Interfaces;

public interface IWeatherApiService
{
    WeatherResponse GetData(string lat, string lon);
}