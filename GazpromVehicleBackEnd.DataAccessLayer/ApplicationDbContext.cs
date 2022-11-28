using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GazpromVehicleBackEnd.DataAccessLayer;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Defect> Defects { get; set; }
    public DbSet<Sanction> Sanctions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
         Database.EnsureDeleted();
         Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Defect>()
            .HasOne(u => u.Sanction)
            .WithOne(p => p.Defect)
            .HasForeignKey<Sanction>(p => p.Id);
        
        base.OnModelCreating(builder);
    }
}