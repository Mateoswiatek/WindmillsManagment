using System.Diagnostics;
using MgWindManager.Models;
using MgWindManager.Models.Dto;
using MgWindManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace windmillsManagement.Controllers;

[Authorize]
public class WindmillController(
    ILogger<WindmillController> logger,
    IWindmillService windmillService,
    IWindParkService windParkService)
    : Controller
{
    // wyświetlenie wszystkich windparków po parametrach "lokalizacji"
    // np będziemy mieli osadzoną mapę, i dostaniemy zakresy szerokości i długości.
    // user może też określić max ilość wiatraków, tak aby nie ładowało się za kazdym razem.
    
    [Route("windmills/{guid}")]
    public IActionResult Windmill(Guid guid)
    {
        logger.LogInformation("guid: {}", guid);
        return View(windmillService.GetByGuid(guid));
    }
    
    //TODO zrobić, aby taki wiatrak był dodawany / oznaczany jako temp, aby admin musiał to zatwierdzić? ewentualnie
    // każdy moze dodawać, ale admin musi to zatwierdzić? trafiają do tabeli "roboczej"
    // i admin ma opcję w styl zatwierdź zmiany, i w tedy to jest zapisywane do zasadniczej.
    // 
    [HttpGet]
    public IActionResult AddWindmill()
    {
        var windparks = windParkService.GetWindParkNamesWithIds();
        var windmill = new Windmill
        {
            Name = "Domyślna nazwa"
        };
        var request = new WindmillAddRequest()
        {
            Windmill = windmill,
            WindParks = windparks
        };
        return View(request);
    }
    
    [HttpPost]
    public IActionResult AddWindmill(Windmill windmill)
    {
        // if (!ModelState.IsValid)
        // {
        //     return View(windmill);
        // }

        if (windmill.WindParkId.HasValue)
        {
            windmill.WindPark = windParkService.GetByGuid(windmill.WindParkId.Value);
        }

        TempData["Guid"] = windmillService.Save(windmill);
        return RedirectToAction("AddWindmill");
    }
    
    //https://localhost:7214/windmills?page=2&size=20
    [Route("windmills")]
    [HttpGet]
    public IActionResult WindmillList(string search, int filter = 0, int page = 1, int size = 5)
    {
        ViewData["filter"] = filter;
        ViewData["search"] = search;
        var pagedWindmills = windmillService.GetPagedWindmillShortDtos(search, filter, page, size);
        return View(pagedWindmills);
    }



    //todo dodać walidację, tak aby tylko admin mógł usuwać.
    // [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            windmillService.Delete(guid);
        }
        catch
        {
            return NotFound();
        }
        
        return RedirectToAction("WindmillList", new { page = 1, size = 5 });
    }

    //Tutaj przychodzimy,
    [HttpGet]
    public IActionResult Edit(Guid guid)
    {
        var windmill = windmillService.GetByGuid(guid);
        // dostajemy widok, w którym możemy modyfikować nasz wiatrak.
        return View(windmill);
    }

    //Nastepnie, z tamtej storny, klikając "zapisz" jesteśmy przekierowani tutaj,
    // jako Post, bo przy Pucie, trzeba by rozbijać obiekt na parametry,
    // bo tylko Post może mieć body. dla uproszczenia tak to jest zrobione.
    //i tu następuje faktyczny zapis nowo zmodyfikowanych danych do bazy.
    [HttpPost]
    public IActionResult Update(Windmill windmill)
    {
        try
        {
            windmillService.Update(windmill);
            return RedirectToAction("Windmill", new {guid = windmill.Guid} );
        }
        catch
        {
            return NotFound();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}