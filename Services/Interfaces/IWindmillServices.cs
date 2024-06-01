using windmillsManagement.Models;
using windmillsManagement.Models.Windmill;

public interface IWindmillServices
{
    Guid Save(Windmill windmill);
}