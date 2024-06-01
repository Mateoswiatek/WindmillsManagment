using MgWindManager.Models;
using MgWindManager.Models.Dto;
using X.PagedList;

public interface IWindmillServices
{
    Guid Save(Windmill windmill);
    IPagedList<Windmill> GetPagedWindmills(int page, int pageSize);
    IPagedList<WindmillShortDto> GetPagedWindmillShortDtos(string name, int filter, int page, int pageSize);
    Windmill GetByGuid(Guid guid);
    void Delete(Guid guid);

    Windmill Update(Windmill newWindmill);
}