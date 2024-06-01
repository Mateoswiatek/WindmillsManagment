namespace windmillsManagement.Models.Equipment;

// Proste wyposarzenie, jak na razie bez rozbijania na Gaśnice, Apteczki, Lonże, Uprzęże.
// Ewentualnie te rzeczy można dać jako dziedziczące po tym, w tedy polimorfizm będzie.
// Wzorzec TPT - Table per Type, typ encji jest określany na podstawie obecności rekordu w konkretnej tabeli.
public class Equipment
{
    public Guid Guid { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string name { get; set; }
}