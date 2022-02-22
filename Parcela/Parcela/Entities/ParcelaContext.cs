using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    /// <summary>
    /// Context za bazu podataka
    /// </summary>
    public class ParcelaContext : DbContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="options"></param>
        public ParcelaContext(DbContextOptions<ParcelaContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// DbSet Parcele
        /// </summary>
        public DbSet<ParcelaEntity> Parcele { get; set; }

        /// <summary>
        /// DbSet ZasticeneZone
        /// </summary>
        public DbSet<ZasticenaZonaEntity> ZasticeneZone { get; set; }

        /// <summary>
        /// DbSet Odvodnjavanja
        /// </summary>
        public DbSet<OdvodnjavanjeEntity> Odvodnjavanja { get; set; }

        /// <summary>
        /// DbSet Obradivosti
        /// </summary>
        public DbSet<ObradivostEntity> Obradivosti { get; set; }

        /// <summary>
        /// DbSet ObliciSvojine
        /// </summary>
        public DbSet<OblikSvojineEntity> ObliciSvojine { get; set; }

        /// <summary>
        /// DbSet Kulture
        /// </summary>
        public DbSet<KulturaEntity> Kulture { get; set; }

        /// <summary>
        /// DbSet Klase
        /// </summary>
        public DbSet<KlasaEntity> Klase { get; set; }

        /// <summary>
        /// DbSet DeloviParcela
        /// </summary>
        public DbSet<DeoParceleEntity> DeloviParcela { get; set; }

        /// <summary>
        /// Kreiranje modela
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<ParcelaEntity>()
                .HasData(new
                {
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Povrsina = 10000,
                    BrojParcele = "111",
                    BrojListaNepokretnosti = "12",
                    KulturaStvarnoStanje = "Njive",
                    KlasaStvarnoStanje = "I",
                    ObradivostStvarnoStanje = "Obradivo",
                    ZasticenaZonaStvarnoStanje = "1",
                    OdvodnjavanjeStvarnoStanje = "Povrsinsko",
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                    KatastarskaOpstinaID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                    KupacID = Guid.Parse("2a411c13-a195-48f7-8dbc-67596c3974c0")
                });

            modelBuilder.Entity<ParcelaEntity>()
                .HasData(new
                {
                    ParcelaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Povrsina = 2000,
                    BrojParcele = "222",
                    BrojListaNepokretnosti = "92",
                    KulturaStvarnoStanje = "Vinogradi",
                    KlasaStvarnoStanje = "II",
                    ObradivostStvarnoStanje = "Obradivo",
                    ZasticenaZonaStvarnoStanje = "2",
                    OdvodnjavanjeStvarnoStanje = "Podzemno",
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                    KatastarskaOpstinaID = Guid.Parse("177E64AD-2FF0-4A40-9C75-1F9B02FFE1E9"),
                    KupacID = Guid.Parse("1a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            modelBuilder.Entity<ParcelaEntity>()
                .HasData(new
                {
                    ParcelaID = Guid.Parse("228927ab-e8fd-4d7e-8986-b9c3c4930480"),
                    Povrsina = 3000,
                    BrojParcele = "333",
                    BrojListaNepokretnosti = "54",
                    KulturaStvarnoStanje = "Vocnjaci",
                    KlasaStvarnoStanje = "III",
                    ObradivostStvarnoStanje = "Ostalo",
                    ZasticenaZonaStvarnoStanje = "3",
                    OdvodnjavanjeStvarnoStanje = "Podzemno",
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                    KatastarskaOpstinaID = Guid.Parse("177E64AD-2FF0-4A40-9C75-1F9B02FFE1E9"),
                    KupacID = Guid.Parse("1a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            modelBuilder.Entity<ParcelaEntity>()
                .HasData(new
                {
                    ParcelaID = Guid.Parse("a913cb6f-9608-474a-88dc-4f38a51315ea"),
                    Povrsina = 4000,
                    BrojParcele = "444",
                    BrojListaNepokretnosti = "63",
                    KulturaStvarnoStanje = "Njive",
                    KlasaStvarnoStanje = "I",
                    ObradivostStvarnoStanje = "Obradivo",
                    ZasticenaZonaStvarnoStanje = "1",
                    OdvodnjavanjeStvarnoStanje = "Povrsinsko",
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                    KatastarskaOpstinaID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                    KupacID = Guid.Parse("2a411c13-a195-48f7-8dbc-67596c3974c0")
                });

            modelBuilder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                    ZasticenaZonaOznaka = 1
                });

            modelBuilder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("9eec3d7d-2f21-4719-a8db-415806748dfb"),
                    ZasticenaZonaOznaka = 2
                });

            modelBuilder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("9d994da6-a766-4d67-971b-3b589b1ecbf8"),
                    ZasticenaZonaOznaka = 3
                });

            modelBuilder.Entity<ZasticenaZonaEntity>()
                .HasData(new
                {
                    ZasticenaZonaID = Guid.Parse("28ea362f-4e6c-4e0e-b853-b79c509a6b16"),
                    ZasticenaZonaOznaka = 4
                });

            modelBuilder.Entity<OdvodnjavanjeEntity>()
                .HasData(new
                {
                    OdvodnjavanjeID = Guid.Parse("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                    OdvodnjavanjeNaziv = "Povrsinsko"
                });

            modelBuilder.Entity<OdvodnjavanjeEntity>()
                .HasData(new
                {
                    OdvodnjavanjeID = Guid.Parse("a2f44a7b-cdfb-4d69-b651-6d715afe8217"),
                    OdvodnjavanjeNaziv = "Podzemno"
                });

            modelBuilder.Entity<ObradivostEntity>()
                .HasData(new
                {
                    ObradivostID = Guid.Parse("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                    ObradivostNaziv = "Obradivo"
                });

            modelBuilder.Entity<ObradivostEntity>()
                .HasData(new
                {
                    ObradivostID = Guid.Parse("bf45ffef-1166-44fb-a2e1-67824a6561f2"),
                    ObradivostNaziv = "Ostalo"
                });

            modelBuilder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                    OblikSvojineNaziv = "Privatno"
                });

            modelBuilder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("91a1f792-bc28-4f6e-bdda-cb577d7858fe"),
                    OblikSvojineNaziv = "Drzavna svojina RS"
                });

            modelBuilder.Entity<OblikSvojineEntity>()
               .HasData(new
               {
                   OblikSvojineID = Guid.Parse("8cd557cd-b1ce-4a6e-8491-ddc80310d1e7"),
                   OblikSvojineNaziv = "Drzavna svojina"
               });

            modelBuilder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("11474cc8-1ac3-47b5-87f0-c7de7f29f024"),
                    OblikSvojineNaziv = "Drustvena svojina"
                });

            modelBuilder.Entity<OblikSvojineEntity>()
               .HasData(new
               {
                   OblikSvojineID = Guid.Parse("64bda426-0a91-44fa-8da9-93cf24cc93ae"),
                   OblikSvojineNaziv = "Zadruzna svojina"
               });

            modelBuilder.Entity<OblikSvojineEntity>()
                .HasData(new
                {
                    OblikSvojineID = Guid.Parse("2d83379c-87e6-45a1-9f00-321b820062fc"),
                    OblikSvojineNaziv = "Mesovita svojina"
                });

            modelBuilder.Entity<OblikSvojineEntity>()
              .HasData(new
              {
                  OblikSvojineID = Guid.Parse("085f566f-9900-45e4-800a-d679331b8050"),
                  OblikSvojineNaziv = "Drugi oblici"
              });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                    KulturaNaziv = "Njive"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("86f5706f-737b-4b20-beed-531aa64326cb"),
                    KulturaNaziv = "Vrtovi"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("36840f4a-91c7-48b2-a85e-c2f285db0a56"),
                    KulturaNaziv = "Vocnjaci"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("04e29d95-7330-4d42-a10a-08556d478a46"),
                    KulturaNaziv = "Vinogradi"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("67db5b31-6cb1-4bd0-a6a9-52702b06ced4"),
                    KulturaNaziv = "Livade"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("e7977a9e-74c7-4f4b-91b7-57fc03159456"),
                    KulturaNaziv = "Pasnjaci"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("cb674d70-bd30-4ed5-bcc0-b5db489bfbe7"),
                    KulturaNaziv = "Sume"
                });

            modelBuilder.Entity<KulturaEntity>()
                .HasData(new
                {
                    KulturaID = Guid.Parse("5f4a3a1e-3406-4991-abd4-0f095b59ac84"),
                    KulturaNaziv = "Trstici-mocvare"
                });

            modelBuilder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                    KlasaOznaka = "I"
                });

            modelBuilder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("18227841-6ba9-4509-b8fa-faa8f6699b3b"),
                    KlasaOznaka = "II"
                });

            modelBuilder.Entity<KlasaEntity>()
               .HasData(new
               {
                   KlasaID = Guid.Parse("b417f2f5-5b3a-4856-a140-49a361d4cfd5"),
                   KlasaOznaka = "III"
               });

            modelBuilder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("1d6f312f-73e5-4a57-9dd5-ba31f08bb967"),
                    KlasaOznaka = "IV"
                });

            modelBuilder.Entity<KlasaEntity>()
               .HasData(new
               {
                   KlasaID = Guid.Parse("560475bd-3701-405b-9be8-e89768ce3eb5"),
                   KlasaOznaka = "V"
               });

            modelBuilder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("d2c9a6ad-8c0d-44d2-8b56-2c4eada4ff99"),
                    KlasaOznaka = "VI"
                });

            modelBuilder.Entity<KlasaEntity>()
               .HasData(new
               {
                   KlasaID = Guid.Parse("51c6ad49-5b78-424d-9cf5-259cb7d9e0e0"),
                   KlasaOznaka = "VII"
               });

            modelBuilder.Entity<KlasaEntity>()
                .HasData(new
                {
                    KlasaID = Guid.Parse("9a011828-2b22-4666-b300-fe98e2c94d9a"),
                    KlasaOznaka = "VIII"
                });

            modelBuilder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("cae99a88-c6ee-4f4c-a463-419ac8fc1b85"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 2000,
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            modelBuilder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("c1df8ec6-dcae-4b27-bb33-7539fb6125c0"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 3000,
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            modelBuilder.Entity<DeoParceleEntity>()
               .HasData(new
               {
                   DeoParceleID = Guid.Parse("910876c8-ab40-4a29-a047-f9913ebaefb8"),
                   RedniBroj = 3,
                   PovrsinaDelaParcele = 4000,
                   ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0")
               });

            modelBuilder.Entity<DeoParceleEntity>()
                .HasData(new
                {
                    DeoParceleID = Guid.Parse("2884b2b0-302c-4eac-847c-65e4c356132b"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 2000,
                    ParcelaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b")
                });
        }
    }
}
