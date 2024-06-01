using MgWindManager.Models;
using MgWindManager.Models.Dto;
using X.PagedList;

public interface IWindmillService
{
    Guid Save(Windmill windmill);
    List<WindmillShortDto> GetPagedWindmillsByWindParkGuid(Guid windParkGuid);
    IPagedList<WindmillShortDto> GetPagedWindmillShortDtos(string name, int filter, int page, int pageSize);
    Windmill GetByGuid(Guid guid);
    void Delete(Guid guid);

    Windmill Update(Windmill newWindmill);
}