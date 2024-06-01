using MgWindManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MgWindManager;

public class MgWindCtx : IdentityDbContext<UserModel>
{
    public MgWindCtx(DbContextOptions<MgWindCtx> options) : base(options)
    {
    }
    
    public DbSet<Windmill> Windmills { get; set; }
    public DbSet<WindPark> WindParks { get; set; }
    // public DbSet<Equipment.Equipment> Equipments { get; set; } = null!;
    // public DbSet<Visit.Visit> Visits { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<WindPark>()
            .HasMany(e => e.Windmills)
            .WithOne(e => e.WindPark)
            .HasForeignKey(e => e.WindParkId);
    }
}