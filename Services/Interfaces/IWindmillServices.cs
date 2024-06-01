using MgWindManager.Models.Dto;
using windmillsManagement.Models;
using windmillsManagement.Models.Windmill;
using X.PagedList;

public interface IWindmillServices
{
    Guid Save(Windmill windmill);
    IPagedList<Windmill> GetPagedWindmills(int page, int pageSize);
    IPagedList<WindmillShortDto> getPagedWindmillShortDtos(int page, int pageSize);
}