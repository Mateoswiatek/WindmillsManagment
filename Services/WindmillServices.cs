using windmillsManagement.Models;
using windmillsManagement.Models.Windmill;

public class WindmillServices : IWindmillServices
{
    public Guid Save(Windmill windmill)
    {
        return Guid.NewGuid();
    }
}