using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MgWindManager.Models;

namespace MgWindManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Name = "Moje Imię";
        ViewData["Nazwa2"] = "Moje Drugie Imie 123";
        TempData["NazwaTemp"] = "Moja nazwa Tempowa";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}