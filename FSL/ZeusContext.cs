public class ZeusContext : DbContext
using Microsoft.EntityFrameworkCore;
using FSL.Entities;

namespace FSL
    {
        public class ZeusContext : DbContext
        {
            public ZeusContext(DbContextOptions<ZeusContext> options) : base(options)
            {
            }

            public DbSet<TestaScontrino> TestaScontrini { get; set; } = null!;
            public DbSet<RigaScontrino> RigheScontrini { get; set; } = null!;
            public DbSet<VenCassa> VenCasse { get; set; } = null!;

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Configuration for VenCassa
                modelBuilder.Entity<VenCassa>(entity =>
                {
                    entity.ToTable("ven Cassa");

                    entity.HasKey(e => e.IdCassa);
                    entity.Property(e => e.IdCassa).HasColumnName("Id Cassa");
                    entity.Property(e => e.NumeroCassa).HasColumnName("Numero Cassa");
                    entity.Property(e => e.Descrizione).HasColumnName("Descrizione").HasMaxLength(100);
                    entity.Property(e => e.DataAttivazione).HasColumnName("Data Attivazione");
                });

                // Configuration for TestaScontrino
                modelBuilder.Entity<TestaScontrino>(entity =>
                {
                    entity.ToTable("Testa Scontrini");

                    entity.HasKey(e => e.IdScontrino);
                    entity.Property(e => e.IdScontrino).HasColumnName("Id Scontrino");
                    entity.Property(e => e.NumeroScontrino).HasColumnName("Numero Scontr").HasMaxLength(50);
                    entity.Property(e => e.DataMovimento).HasColumnName("Data Movimento");
                    entity.Property(e => e.IdCassa).HasColumnName("Id Cassa");

                    // Definizione precisione decimale per l'importo totale
                    entity.Property(e => e.ImportoTotale)
                          .HasColumnName("Importo Totale")
                          .HasPrecision(18, 2);

                    // Relazione 1-N: VenCassa -> TestaScontrini
                    entity.HasOne(d => d.Cassa)
                          .WithMany(p => p.TestaScontrini)
                          .HasForeignKey(d => d.IdCassa)
                          .OnDelete(DeleteBehavior.Restrict); // Evita cancellazioni a catena distruttive sulle casse
                });

                // Configuration for RigaScontrino
                modelBuilder.Entity<RigaScontrino>(entity =>
                {
                    entity.ToTable("Righe Scontrini");

                    entity.HasKey(e => e.IdRiga);
                    entity.Property(e => e.IdRiga).HasColumnName("Id Riga");
                    entity.Property(e => e.IdScontrino).HasColumnName("Id Scontrino");
                    entity.Property(e => e.CodiceProdotto).HasColumnName("Codice Prodotto").HasMaxLength(50);
                    entity.Property(e => e.DescrizioneArticolo).HasColumnName("Descrizione Articolo").HasMaxLength(200);

                    // Precisione decimali per i calcoli della riga
                    entity.Property(e => e.Quantita).HasColumnName("Quantita").HasPrecision(18, 3); // 3 decimali per gestire eventuali pesi/frazioni
                    entity.Property(e => e.PrezzoUnitario).HasColumnName("Prezzo Unitario").HasPrecision(18, 2);
                    entity.Property(e => e.Importo).HasColumnName("Importo").HasPrecision(18, 2);

                    // Relazione 1-N: TestaScontrino -> RigheScontrini
                    entity.HasOne(d => d.TestaScontrino)
                          .WithMany(p => p.RigheScontrini)
                          .HasForeignKey(d => d.IdScontrino)
                          .OnDelete(DeleteBehavior.Cascade); // Se elimini la testa dello scontrino, elimina anche le sue righe
                });
            }
        }
    }
