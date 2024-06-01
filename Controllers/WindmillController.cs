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
    
    [Route("windmills/{guid}")]
    public IActionResult Windmill(Guid guid)
    {
        logger.LogInformation("guid: {}", guid);
        return View(windmillServices.GetByGuid(guid));
    }
    
    //TODO zrobić, aby taki wiatrak był dodawany / oznaczany jako temp, aby admin musiał to zatwierdzić? ewentualnie
    // każdy moze dodawać, ale admin musi to zatwierdzić? trafiają do tabeli "roboczej"
    // i admin ma opcję w styl zatwierdź zmiany, i w tedy to jest zapisywane do zasadniczej.
    // 
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
        return RedirectToAction("AddWindmill");
    }
    
    //https://localhost:7214/windmills?page=2&size=20
    [Route("windmills")]
    [HttpGet]
    public IActionResult WindmillList(string search, int filter = 0, int page = 1, int size = 5)
    {
        ViewData["filter"] = filter;
        ViewData["search"] = search;
        var pagedWindmills = windmillServices.GetPagedWindmillShortDtos(search, filter, page, size);
        return View(pagedWindmills);
    }



    //todo dodać walidację, tak aby tylko admin mógł usuwać.
    // [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        windmillServices.Delete(guid);
        
        
        return RedirectToAction("WindmillList", new { page = 1, size = 5 });
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