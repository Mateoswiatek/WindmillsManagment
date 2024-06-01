using Microsoft.AspNetCore.Mvc;

namespace windmillsManagement.Controllers;

public class WindParkController : Controller
{
    //
    
    // lista wszystkich windparków, sortowana.
    // ewentualnie na mapie wbudowanej, można by dać zaznaczenie kolorkami różńymi poszczególnyc windparków.
    // shortDto
    
    //po id / nazwie konkretnego windparku.
    // ogólne informacje, liczba wiatraków, opis, no wszystko tu leci zwracamy też listę wszystkich wiatraków 
    // Dto 
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
}