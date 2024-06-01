using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgWindManager.Models;

public class Windmill
{
    [Key]
    public Guid Guid { get; set; }

    [Required(ErrorMessage = "Podaj nazwę / oznaczenie wiatraka!")]
    public string Name { get; set; }
    public string ?Description { get; set; }
    [Required]
    public string Latitude { get; set; }
    [Required]
    public string Longitude { get; set; }
    public double Height { get; set; }
    public DateTime ?DateOfLastVisit { get; set; }
    public DateTime ?DateAndTimeAddedToDelete { get; set; }
    
    //Baaardzo na okrągło. Po co mi i to i to?!??
    public Guid? WindParkId { get; set; }
    [ForeignKey("WindParkId")]
    public WindPark ?WindPark { get; set; }
    // public ISet<Equipment.Equipment> ?WindmillEquipments { get; set; }
    // public ISet<Visit.Visit> ?Visits { get; set; }
}