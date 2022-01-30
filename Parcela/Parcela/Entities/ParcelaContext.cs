using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class ParcelaContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ParcelaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<ParcelaEntity> Parcele { get; set; }

        public DbSet<ZasticenaZonaEntity> ZasticeneZone { get; set; }

        public DbSet<OdvodnjavanjeEntity> Odvodnjavanja { get; set; }

        public DbSet<ObradivostEntity> Obradivosti { get; set; }

        public DbSet<OblikSvojineEntity> ObliciSvojine { get; set; }

        public DbSet<KulturaEntity> Kulture { get; set; }

        public DbSet<KlasaEntity> Klase { get; set; }

        public DbSet<DeoParceleEntity> DeloviParcela { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ParcelaDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ParcelaEntity>()
                .HasData(new
                {
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Povrsina = 1000,
                    BrojParcele = "12345",
                    BrojListaNepokretnosti = "12345",
                    KulturaStvarnoStanje = "Kukuruz",
                    KlasaStvarnoStanje = "Klasa1",
                    ObradivostStvarnoStanje = "Obradivost1",
                    ZasticenaZonaStvarnoStanje = "ZasticenaZona1",
                    OdvodnjavanjeStvarnoStanje = "Odvodnjavanje1"
                });

            builder.Entity<ParcelaEntity>()
                .HasData(new
                {
                    ParcelaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Povrsina = 2000,
                    BrojParcele = "54321",
                    BrojListaNepokretnosti = "54321",
                    KulturaStvarnoStanje = "Soja",
                    KlasaStvarnoStanje = "Klasa2",
                    ObradivostStvarnoStanje = "Obradivost2",
                    ZasticenaZonaStvarnoStanje = "ZasticenaZona2",
                    OdvodnjavanjeStvarnoStanje = "Odvodnjavanje2"
                });

            builder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ZasticenaZonaOznaka = 1
                });

            builder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ZasticenaZonaOznaka = 2
                });

            builder.Entity<OdvodnjavanjeEntity>()
                .HasData(new
                {
                    OdvodnjavanjeID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje1"
                });

            builder.Entity<OdvodnjavanjeEntity>()
                .HasData(new
                {
                    OdvodnjavanjeID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje2"
                });

            builder.Entity<ObradivostEntity>()
                .HasData(new
                {
                    ObradivostID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ObradivostNaziv = "Obradivost1"
                });

            builder.Entity<ObradivostEntity>()
                .HasData(new
                {
                    ObradivostID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ObradivostNaziv = "Obradivost2"
                });

            builder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    OblikSvojineNaziv = "Oblik svojine 1"
                });

            builder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    OblikSvojineNaziv = "Oblik svojine 2"
                });

            builder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    KulturaNaziv = "Kukuruz"
                });

            builder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    KulturaNaziv = "Soja"
                });

            builder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    KlasaOznaka = 1
                });

            builder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    KlasaOznaka = 2
                });

            builder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 1000
                });

            builder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    RedniBroj = 2,
                    PovrsinaDelaParcele = 2000
                });
        }
    }
}
