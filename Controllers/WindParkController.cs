using MgWindManager.Models;
using MgWindManager.Models.Dto;
using MgWindManager.Services;
using MgWindManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WindParksManagement.Controllers;

[Authorize]
public class WindParkController(
    ILogger<WindParkController> logger,
    IWindParkService windParkService,
    IWindmillService windmillService)
    : Controller
{
    //
    
    // lista wszystkich windparków, sortowana.
    // ewentualnie na mapie wbudowanej, można by dać zaznaczenie kolorkami różńymi poszczególnyc windparków.
    // shortDto
    
    //po id / nazwie konkretnego windparku.
    // ogólne informacje, liczba wiatraków, opis, no wszystko tu leci zwracamy też listę wszystkich wiatraków 
    // Dto 
    
    
    
     // wyświetlenie wszystkich windparków po parametrach "lokalizacji"
    // np będziemy mieli osadzoną mapę, i dostaniemy zakresy szerokości i długości.
    // user może też określić max ilość wiatraków, tak aby nie ładowało się za kazdym razem.
    
    [Route("windparks/{guid}")]
    public IActionResult WindPark(Guid guid)
    {
        logger.LogInformation("guid: {}", guid);

        var response = new WindParkViewModel()
        {
            WindPark = windParkService.GetByGuid(guid),
            Windmills = windmillService.GetPagedWindmillsByWindParkGuid(guid)
        };
        return View(response);
    }
    
    [HttpGet]
    public IActionResult AddWindPark()
    {
        
        var windPark = new WindPark
        {
            Name = "Domyślna nazwa Windparku"
            //mozna by dodac do windparku metodke aby dodawac wiatraki do windparku.
        };
        return View(windPark);
    }
    
    [HttpPost]
    public IActionResult AddWindPark(WindPark windPark)
    {
        // if (!ModelState.IsValid)
        // {
        //     return View(windPark);
        // }

        TempData["Guid"] = windParkService.Save(windPark);
        return RedirectToAction("AddWindPark");
    }
    
    //https://localhost:7214/windparks?page=2&size=20
    [Route("windparks")]
    [HttpGet]
    public IActionResult WindParkList(string search, int filter = 0, int page = 1, int size = 5)
    {
        ViewData["filter"] = filter;
        ViewData["search"] = search;
        var pagedWindParks = windParkService.GetPagedWindParkShortDtos(search, filter, page, size);
        return View(pagedWindParks);
    }
    
    public IActionResult Delete(Guid guid)
    {
        try
        {
            windParkService.Delete(guid);
        }
        catch
        {
            return NotFound();
        }
        
        return RedirectToAction("WindParkList", new { page = 1, size = 5 });
    }
    
    [HttpGet]
    public IActionResult Edit(Guid guid)
    {
        var windPark = windParkService.GetByGuid(guid);
        return View(windPark);
    }
    
    [HttpPost]
    public IActionResult Update(WindPark windPark)
    {
        try
        {
            windParkService.Update(windPark);
            return RedirectToAction("WindPark", new {guid = windPark.Guid} );
        }
        catch
        {
            return NotFound();
        }
    }
  
}