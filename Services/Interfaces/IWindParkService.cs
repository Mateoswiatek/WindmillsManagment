using MgWindManager.Models;
using MgWindManager.Models.Dto;
using X.PagedList;

namespace MgWindManager.Services.Interfaces;

public interface IWindParkService
{
    Guid Save(WindPark windmill);
    List<WindParkNameWithIdDto> GetWindParkNamesWithIds();
    IPagedList<WindParkShortDto> GetPagedWindParkShortDtos(string search, int filter, int page, int pageSize);
    WindPark GetByGuid(Guid guid);
    void Delete(Guid guid);

    WindPark Update(WindPark newWindPark);
}