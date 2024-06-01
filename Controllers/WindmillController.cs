using System.Diagnostics;
using MgWindManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using windmillsManagement.Models;
using windmillsManagement.Models.Windmill;

namespace windmillsManagement.Controllers;

public class WindmillController : Controller
{
    private readonly ILogger<WindmillController> _logger;
    private readonly IWindmillServices _windmillServices;

    // wyświetlenie wszystkich windparków po parametrach "lokalizacji"
    // np będziemy mieli osadzoną mapę, i dostaniemy zakresy szerokości i długości.
    // user może też określić max ilość wiatraków, tak aby nie ładowało się za kazdym razem.
    
    // po prostu zrobimy wyszukiwanie z filtrem. i w tedy w windparkach, będziemy zwracać efekt takiego
    // wyszukania, gdzie windpark==this.
    
    public WindmillController(ILogger<WindmillController> logger, 
        IWindmillServices windmillServices)
    {
        _logger = logger;
        _windmillServices = windmillServices;
    }

    [Route("windmills/{guid}")]
    public IActionResult Windmill(Guid guid)
    {
        _logger.LogInformation("guid: {}", guid);

        var windmill = new Windmill()
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

        var guid = _windmillServices.Save(windmill);
        
        
        // Logika dodawania Encji do bazy danych

        TempData["Guid"] = guid;
        
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
        _logger.LogInformation("tutaj będzie sobie lista , page: {}, size: {}", page, size);
        
        //Tutaj np będzie zwracana również nazwa windparku / numer, i po nim bedzie przekierowanie, choc to to 
        // już po stornie fronta można zorbić.
        var windmills = MockWindmills();
        
        var pageNumber = page ?? 1;
        var pageSize = size ?? 5;
        var pagedWindmills = windmills.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        // _windmillService.GetPagedWindmills(pageNumber, currentPageSize);
        
        
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
    
    
    // Metoda zwracająca zmockowaną listę wiatraków
    private static List<Windmill> MockWindmills()
    {
        var windmills = new List<Windmill>();
        for (int i = 0; i < 20; i++)
        {
            var windmill = new Windmill()
            {
                Guid = Guid.NewGuid(),
                Name = "Windmill " + i,
                Description = "Opis przykładowego",
                Latitude = 0,
                Longitude = 0,
                Height = 100.25,
                DateOfLastVisit = DateTime.Now,
                // WindPark = null,
                // WindmillEquipments = null,
                // Visits = null
            };
            windmills.Add(windmill);
        }
        return windmills;
    }
}