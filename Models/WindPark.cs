namespace windmillsManagement.Models.WindPark;

public class WindPark
{
    public string Owner { get; set; }
    public string ContactDetails { get; set; }
    // czy to na pewno powinno być? mozna zwracać pozycję pierwszego z seta?
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ISet<Windmill.Windmill> Windmills { get; set; }
}