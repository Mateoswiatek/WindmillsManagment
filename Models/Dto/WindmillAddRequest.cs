namespace MgWindManager.Models.Dto;

public class WindmillAddRequest
{
    public Windmill Windmill { get; set; }
    public List<WindParkNameWithIdDto> WindParks { get; set; }
}