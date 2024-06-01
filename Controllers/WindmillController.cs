using System.Diagnostics;
using MgWindManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using windmillsManagement.Models;
using windmillsManagement.Models.Windmill;

namespace windmillsManagement.Controllers;

public class WindmillController(
    ILogger<WindmillController> logger,
    IWindmillServices windmillServices)
    : Controller
{
    // wyświetlenie wszystkich windparków po parametrach "lokalizacji"
    // np będziemy mieli osadzoną mapę, i dostaniemy zakresy szerokości i długości.
    // user może też określić max ilość wiatraków, tak aby nie ładowało się za kazdym razem.
    
    // po prostu zrobimy wyszukiwanie z filtrem. i w tedy w windparkach, będziemy zwracać efekt takiego
    // wyszukania, gdzie windpark==this.

    [Route("windmills/{guid}")]
    public IActionResult Windmill(Guid guid)
    {
        logger.LogInformation("guid: {}", guid);

        var windmill = new Windmill
        {
            Guid = default,
            Name = "nazwa1",
            Description = "Opis przykładowego",
            Latitude = 0,
            Longitude = 0,
            Height = 100.25,
            DateOfLastVisit = default,
            // WindPark = null,
            // WindmillEquipments = null,
            // Visits = null
        };
        
        return View(windmill);
    }

    //TODO czy tak się powinno obsługiwać formularze?
    [HttpGet]
    public IActionResult AddWindmill()
    {
        
        var windmill = new Windmill
        {
            Name = "Domyślna nazwa"
        };
        return View(windmill);
    }
    
    [HttpPost]
    public IActionResult AddWindmill(Windmill windmill)
    {
        // if (!ModelState.IsValid)
        // {
        //     return View(windmill);
        // }

        TempData["Guid"] = windmillServices.Save(windmill);
        
        //To zawsze będzie metodą get, więc zawsze nam wróci
        // do pustego formularza i straci kontekst przekazanych
        // danych
        return RedirectToAction("AddWindmill");
    }
    
    //https://localhost:7214/windmills?page=2&size=20
    [Route("windmills")]
    [HttpGet]
    public IActionResult WindmillList(int? page, int? size)
    {
        logger.LogInformation("tutaj będzie sobie lista , page: {}, size: {}", page, size);
        
        var pageNumber = page ?? 1;
        var pageSize = size ?? 5;

        var pagedWindmills = windmillServices.getPagedWindmillShortDtos(pageNumber, pageSize);
        
        return View(pagedWindmills);
    }

    public IActionResult Redirect()
    {
        return RedirectToAction("WindmillList");
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}