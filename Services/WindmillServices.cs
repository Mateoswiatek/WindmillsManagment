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
    public IPagedList<WindmillShortDto> getPagedWindmillShortDtos(int page, int pageSize)
    {
        return ctx.Windmills
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
    }
    
}