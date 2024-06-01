using Microsoft.EntityFrameworkCore;
using windmillsManagement.Models.Windmill;
using windmillsManagement.Models.WindPark;

namespace MgWindManager;

public class MgWindCtx : DbContext
{
    public MgWindCtx(DbContextOptions<MgWindCtx> options) : base(options)
    {
    }
    
    public DbSet<Windmill> Windmills { get; set; } = null!;
    // public DbSet<WindPark> WindParks { get; set; } = null!;
    // public DbSet<Equipment.Equipment> Equipments { get; set; } = null!;
    // public DbSet<Visit.Visit> Visits { get; set; } = null!;
}