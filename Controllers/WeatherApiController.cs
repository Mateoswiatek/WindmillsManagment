using MgWindManager.Models;
using MgWindManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace MgWindManager.Controllers;

[Authorize]
public class WeatherApiController(IWeatherApiService weatherApiService) : Controller
{
    public IActionResult Index(string lat, string lon)
    {
        lat = "44.34";
        lon = "10.99";
        WeatherResponse respponse = weatherApiService.GetData(lat, lon);
        return View(respponse);
    }
}