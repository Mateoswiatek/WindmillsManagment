namespace MgWindManager.Models.Dto;

public class WindParkShortDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public bool Deleted { get; set; }
}