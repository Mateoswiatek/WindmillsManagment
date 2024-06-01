using MgWindManager.Models.Dto;
using windmillsManagement.Models.Windmill;
using X.PagedList;

namespace MgWindManager.Services;

public class WindmillServices(MgWindCtx ctx) : IWindmillServices
{
    public Guid Save(Windmill windmill)
    {
        ctx.Windmills.Add(windmill);
        ctx.SaveChanges();

        return windmill.Guid;
    }

    [Obsolete]
    IPagedList<Windmill> IWindmillServices.GetPagedWindmills(int page, int pageSize)
    {
        return ctx.Windmills.ToPagedList(page, pageSize);
    }

    //Projekcja, aby pobierać tylko to co potrzebujemy, aby nie zwracać całych wiatrakow.
    public IPagedList<WindmillShortDto> GetPagedWindmillShortDtos(string search, int filter, int page, int pageSize)
    {
        // ewentualnie jakiegoś buildera? może ładniej by to wyglądało
        
        var query = ctx.Windmills.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(w => w.Name.Contains(search));
        }
        
        switch (filter)
        {
            case 1: // Aktywne
                query = query.Where(w => !w.DateAndTimeAddedToDelete.HasValue);
                break;
            case 2: // Usunięte
                query = query.Where(w => w.DateAndTimeAddedToDelete.HasValue);
                break;
            default: // Wszystkie (0) - nie trzeba dodatkowych filtrów
                break;
        }
        
        var result = query
            .OrderBy(w => w.Name)
            .Select(w => new WindmillShortDto
            {
                Guid = w.Guid,
                Name = w.Name,
                Latitude = w.Latitude,
                Longitude = w.Longitude,
                Height = w.Height,
                Deleted = w.DateAndTimeAddedToDelete.HasValue
            })
            .ToPagedList(page, pageSize);

        return result;
    }

    public Windmill GetByGuid(Guid guid)
    { 
        var windmill = ctx.Windmills.Find(guid);
        
        //TODO własna obsługa błędów, aż do frontu, albo jakiś stream ? cokolwiek XD
        if (windmill == null)
        {
            throw new Exception($"Windmill with GUID {guid} not found.");
        }

        return windmill;
    }

    public void Delete(Guid guid)
    {
        var windmill = ctx.Windmills.Find(guid);
        if (windmill == null)
        {
            throw new Exception($"Windmill with GUID {guid} not found.");
        }
        windmill.DateAndTimeAddedToDelete = DateTime.UtcNow;
        
        if (ctx.SaveChanges() < 1)
        {
            throw new Exception($"Db error when Removing Windmill with GUID {guid}.");
        }
    }
}