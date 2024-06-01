using System.ComponentModel.DataAnnotations;

namespace MgWindManager.Models;

public class WindPark
{
    [Key]
    public Guid Guid { get; set; }
    [Required(ErrorMessage = "Podaj nazwę / oznaczenie Windparku!")]
    public string Name { get; set; }
    public string Owner { get; set; }
    public string ContactDetails { get; set; }
    // czy to na pewno powinno być? mozna zwracać pozycję pierwszego z seta?
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public DateTime ?DateAndTimeAddedToDelete { get; set; }
    public ICollection<Windmill> Windmills { get; set; } = new List<Windmill>(); // ISet
}