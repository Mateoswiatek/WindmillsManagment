namespace MgWindManager.Models.Dto;

public class WindmillShortDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public double Height { get; set; }
    public bool Deleted { get; set; }
}