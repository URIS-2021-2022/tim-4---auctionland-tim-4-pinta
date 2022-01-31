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
        public ParcelaContext(DbContextOptions<ParcelaContext> options) : base(options)
        {
            
        }

        public DbSet<ParcelaEntity> Parcele { get; set; }

        public DbSet<ZasticenaZonaEntity> ZasticeneZone { get; set; }

        public DbSet<OdvodnjavanjeEntity> Odvodnjavanja { get; set; }

        public DbSet<ObradivostEntity> Obradivosti { get; set; }

        public DbSet<OblikSvojineEntity> ObliciSvojine { get; set; }

        public DbSet<KulturaEntity> Kulture { get; set; }

        public DbSet<KlasaEntity> Klase { get; set; }

        public DbSet<DeoParceleEntity> DeloviParcela { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("ParcelaDB"));
        //}

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
                    OdvodnjavanjeStvarnoStanje = "Odvodnjavanje1",
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc")
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
                    OdvodnjavanjeStvarnoStanje = "Odvodnjavanje2",
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc")
                });

            builder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    ZasticenaZonaOznaka = 1
                });

            builder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("9eec3d7d-2f21-4719-a8db-415806748dfb"),
                    ZasticenaZonaOznaka = 2
                });

            builder.Entity<OdvodnjavanjeEntity>()
                .HasData(new
                {
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje1"
                });

            builder.Entity<OdvodnjavanjeEntity>()
                .HasData(new
                {
                    OdvodnjavanjeID = Guid.Parse("a2f44a7b-cdfb-4d69-b651-6d715afe8217"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje2"
                });

            builder.Entity<ObradivostEntity>()
                .HasData(new
                {
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    ObradivostNaziv = "Obradivost1"
                });

            builder.Entity<ObradivostEntity>()
                .HasData(new
                {
                    ObradivostID = Guid.Parse("bf45ffef-1166-44fb-a2e1-67824a6561f2"),
                    ObradivostNaziv = "Obradivost2"
                });

            builder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    OblikSvojineNaziv = "Oblik svojine 1"
                });

            builder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("91a1f792-bc28-4f6e-bdda-cb577d7858fe"),
                    OblikSvojineNaziv = "Oblik svojine 2"
                });

            builder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KulturaNaziv = "Kukuruz"
                });

            builder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("86f5706f-737b-4b20-beed-531aa64326cb"),
                    KulturaNaziv = "Soja"
                });

            builder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                    KlasaOznaka = 1
                });

            builder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("18227841-6ba9-4509-b8fa-faa8f6699b3b"),
                    KlasaOznaka = 2
                });

            builder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("cae99a88-c6ee-4f4c-a463-419ac8fc1b85"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 1000,
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            builder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("2884b2b0-302c-4eac-847c-65e4c356132b"),
                    RedniBroj = 2,
                    PovrsinaDelaParcele = 2000,
                    ParcelaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b")
                });
        }
    }
}
