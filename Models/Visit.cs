namespace windmillsManagement.Models.Visit;

// Bazowa, po niej będą dziedziczyć zarówno Naprawy jak i Przeglądy
public class Visit
{
    public Windmill.Windmill? Windmill { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ExternalDeadline { get; set; } // okreslenie, do kiedy trzeba to zlecenie wykonać.
    public DateTime PlannedExecutionDate { get; set; } // ewentualnie sama data. ale tak lepiej, bo wiadomo jaka kolejność.
    public TimeSpan EstimatedExecutionTime { get; set; } // przewidywany czas wykonania. 
    public TimeSpan RealExecutionTime { get; set; } // przewidywany czas wykonania. 
    public ISet<Equipment.Equipment>? Equipments { get; set; } // np 3 gasnice, albo konkretny dźwig. itp itd
}

// jakaś data, ewentualnie uwagi, jakies inne parametry.
// naprawy będą miały stany "zgłoszone / przyjęte / zaplanowane / odbyte / anulowane".