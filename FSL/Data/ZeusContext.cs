using FSL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSL.Data;

public class ZeusContext : DbContext
{
    public ZeusContext(DbContextOptions<ZeusContext> options)
        : base(options)
    {
    }

    public DbSet<TestaScontrino> TestaScontrini => Set<TestaScontrino>();

    public DbSet<RigaScontrino> RigheScontrini => Set<RigaScontrino>();

    public DbSet<venCassa> Casse => Set<venCassa>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // PK composita RigaScontrino

        modelBuilder.Entity<RigaScontrino>()
            .HasKey(x => new
            {
                x.NumeroRiga,
                x.NumeroScontrino
            });


        // TestaScontrino -> RigaScontrino (1:N)

        modelBuilder.Entity<RigaScontrino>()
            .HasOne(x => x.TestaScontrino)
            .WithMany(x => x.RigheScontrino)
            .HasForeignKey(x => x.NumeroScontrino)
            .HasPrincipalKey(x => x.NumeroScontrino);

        modelBuilder.Entity<venCassa>()
      .HasKey(x => x.IDCassa);

    }
}