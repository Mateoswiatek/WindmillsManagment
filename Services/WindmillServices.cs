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
    public IPagedList<WindmillShortDto> GetPagedWindmillShortDtos(string search, int page, int pageSize)
    {
        // ewentualnie jakiegoś buildera? może ładniej by to wyglądało
        
        var query = ctx.Windmills.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(w => w.Name.Contains(search));
        }
        var result = query
            .OrderBy(w => w.Name)
            .Select(w => new WindmillShortDto
            {
                Guid = w.Guid,
                Name = w.Name,
                Latitude = w.Latitude,
                Longitude = w.Longitude,
                Height = w.Height
            })
            .ToPagedList(page, pageSize);

        return result;
    }

    public Windmill GetByGuid(Guid guid)
    { 
        var windmill = ctx.Windmills.Find(guid);
        
        //TODO własna obsługa błędów, aż do frontu. 
        if (windmill == null)
        {
            throw new Exception($"Windmill with GUID {guid} not found.");
        }

        return windmill;
    }
}