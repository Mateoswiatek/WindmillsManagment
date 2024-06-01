using MgWindManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MgWindManager;

public class MgWindCtx : IdentityDbContext<UserModel>
{
    public MgWindCtx(DbContextOptions<MgWindCtx> options) : base(options)
    {
    }
    
    public DbSet<Windmill> Windmills { get; set; } = null!;
    // public DbSet<WindPark> WindParks { get; set; } = null!;
    // public DbSet<Equipment.Equipment> Equipments { get; set; } = null!;
    // public DbSet<Visit.Visit> Visits { get; set; } = null!;
}