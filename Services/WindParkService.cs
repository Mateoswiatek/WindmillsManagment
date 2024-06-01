using MgWindManager.Models;
using MgWindManager.Models.Dto;
using MgWindManager.Services.Interfaces;
using X.PagedList;

namespace MgWindManager.Services;

public class WindParkService(ILogger<WindParkService> logger, MgWindCtx ctx) : IWindParkService
{
    public Guid Save(WindPark windPark)
    {
        ctx.WindParks.Add(windPark);
        ctx.SaveChanges();

        return windPark.Guid;
    }

    public List<WindParkNameWithIdDto> GetWindParkNamesWithIds()
    {
        return ctx.WindParks.Select(w => new WindParkNameWithIdDto
            {
                Guid = w.Guid,
                Name = w.Name,
            })
            .ToList();
    }

    public IPagedList<WindParkShortDto> GetPagedWindParkShortDtos(string search, int filter, int page, int pageSize)
    {
        var query = ctx.WindParks.AsQueryable();
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
            .Select(w => new WindParkShortDto
            {
                Guid = w.Guid,
                Name = w.Name,
                Owner = w.Owner,
                Latitude = w.Latitude,
                Longitude = w.Longitude,
                Deleted = w.DateAndTimeAddedToDelete.HasValue
            })
            .ToPagedList(page, pageSize);

        return result;
    }

    public WindPark GetByGuid(Guid guid)
    { 
        var windPark = ctx.WindParks.Find(guid);
        
        if (windPark == null)
        {
            throw new Exception($"windPark with GUID {guid} not found.");
        }

        return windPark;
    }

    public void Delete(Guid guid)
    {
        var windPark = ctx.WindParks.Find(guid);
        if (windPark == null)
        {
            throw new Exception($"WindPark with GUID {guid} not found.");
        }
        windPark.DateAndTimeAddedToDelete = DateTime.UtcNow;
        
        if (ctx.SaveChanges() < 1)
        {
            throw new Exception($"Db error when Removing WindPark with GUID {guid}.");
        }
    }

    public WindPark Update(WindPark newWindPark)
    {
        var existingWindPark = ctx.WindParks.Find(newWindPark.Guid);
        if (existingWindPark == null)
        {
            throw new Exception($"Windmill not found!");
        }
        
        existingWindPark.Name = newWindPark.Name;
        existingWindPark.Owner = newWindPark.Owner;
        existingWindPark.ContactDetails = newWindPark.ContactDetails;
        existingWindPark.Latitude = newWindPark.Latitude;
        existingWindPark.Longitude = newWindPark.Longitude;

        ctx.SaveChanges();
        
        return existingWindPark;
    }
}