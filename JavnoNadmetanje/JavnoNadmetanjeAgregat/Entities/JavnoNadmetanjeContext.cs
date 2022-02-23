using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Entities
{
    public class JavnoNadmetanjeContext : DbContext
    {
       

        public JavnoNadmetanjeContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<JavnoNadmetanjeEntity> JavnaNadmetanja { get; set; }

        public DbSet<SluzbeniListEntity> SluzbeniListovi { get; set; }

        public DbSet<StatusJavnogNadmetanjaEntity> StatusiJavnihNadmetanja { get; set; }

        public DbSet<TipJavnogNadmetanjaEntity> TipoviJavnihNadmetanja { get; set; }

       

        /// <summary>
        /// Popunjava bazu podataka inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JavnoNadmetanjeEntity>()
                .HasData(new
                {
                    JavnoNadmetanjeID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                    Datum = DateTime.Parse("27-01-2021"),
                    VremePocetka = DateTime.Parse("24-01-2021"),
                    VremeKraja = DateTime.Parse("28-01-2021"),
                    PocetnaCenaPoHektaru = 1000,
                    PeriodZakupa = 2,
                    Izuzeto = false,
                    TipID = Guid.Parse("4D51C54C-4B90-46DE-8BB2-C8F74FB6FD9E"),
                    StatusID = Guid.Parse("BF50E668-C01A-46E3-BAE8-A1691C23C65F"),
                    Krug = 2,
                    VisinaDopuneDepozita = 10,
                    KatastarskaOpstinaID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                    KupacID = Guid.Parse("2a411c13-a195-48f7-8dbc-67596c3974c0"),
                    ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907")
                });

            modelBuilder.Entity<JavnoNadmetanjeEntity>()
               .HasData(new
               {
                   JavnoNadmetanjeID = Guid.Parse("7C7764E0-27A2-4123-9EB4-081C4E9BCDBF"),
                   Datum = DateTime.Parse("27-01-2021"),
                   VremePocetka = DateTime.Parse("24-01-2021"),
                   VremeKraja = DateTime.Parse("28-01-2021"),
                   PocetnaCenaPoHektaru = 1000,
                   PeriodZakupa = 2,
                   Izuzeto = false,
                   TipID = Guid.Parse("4D51C54C-4B90-46DE-8BB2-C8F74FB6FD9E"),
                   StatusID = Guid.Parse("BF50E668-C01A-46E3-BAE8-A1691C23C65F"),
                   Krug = 2,
                   VisinaDopuneDepozita = 10,
                   KatastarskaOpstinaID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                   KupacID = Guid.Parse("2a411c13-a195-48f7-8dbc-67596c3974c0"),
                   ParcelaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                   AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907")
                   
               }) ;
            modelBuilder.Entity<SluzbeniListEntity>()
                .HasData(new
                {
                    SluzbeniListID = Guid.Parse("102E134D-FFDE-40FA-B355-F0B8BC52F886"),
                    Opstina = "Beograd",
                    BrojSluzbenogLista = 12,
                    DatumIzdavanjaSluzbenogLista = DateTime.Parse("27-01-2021")

                });
            modelBuilder.Entity<SluzbeniListEntity>()
               .HasData(new
               {
                   SluzbeniListID = Guid.Parse("901B0AD2-6AA8-4076-8162-01B3A42F2A2E"),
                   Opstina = "Novi Sad",
                   BrojSluzbenogLista = 13,
                   DatumIzdavanjaSluzbenogLista = DateTime.Parse("27-02-2021")
               });

            modelBuilder.Entity<StatusJavnogNadmetanjaEntity>()
               .HasData(new
               {
                   StatusJavnogNadmetanjaID = Guid.Parse("BF50E668-C01A-46E3-BAE8-A1691C23C65F"),
                   NazivStatusaJavnogNadmetanja = "Status1"
               });
            modelBuilder.Entity<StatusJavnogNadmetanjaEntity>()
              .HasData(new
              {
                  StatusJavnogNadmetanjaID = Guid.Parse("B38E3B4F-5539-4475-8424-00CA7A59E496"),
                  NazivStatusaJavnogNadmetanja = "Status2"
              });
            modelBuilder.Entity<TipJavnogNadmetanjaEntity>()
              .HasData(new
              {
                  TipJavnogNadmetanjaID = Guid.Parse("4D51C54C-4B90-46DE-8BB2-C8F74FB6FD9E"),
                  NazivTipaJavnogNadmetanja = "Tip1"
              });
            modelBuilder.Entity<TipJavnogNadmetanjaEntity>()
              .HasData(new
              {
                  TipJavnogNadmetanjaID = Guid.Parse("0F173F98-C00A-4EB4-8131-AE00177371D8"),
                  NazivTipaJavnogNadmetanja = "Tip2"
              });

        }

    }
}
