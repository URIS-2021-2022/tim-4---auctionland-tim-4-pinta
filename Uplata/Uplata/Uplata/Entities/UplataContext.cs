using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    public class UplataContext : DbContext
    {
        public UplataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UplataEntity> Uplate { get; set; }
        public DbSet<KursEntity> Kursevi { get; set; }


        /// <summary>
        /// Kreiranje modela 
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KursEntity>()
               .HasData(new
               {
                   KursID = Guid.Parse("B06A4284-44E2-46AF-8D74-B79C8B0C6017"),
                   VrednostKursa = 117.8,
                   Datum = DateTime.Now,
                   Valuta = "EUR",
               });

            modelBuilder.Entity<KursEntity>()
               .HasData(new
               {
                   KursID = Guid.Parse("411C4082-CC5E-4F5F-8946-4086EBCA08D0"),
                   VrednostKursa = 150.5,
                   Datum = DateTime.Now,
                   Valuta = "GBT",
               });


            modelBuilder.Entity<UplataEntity>()
                .HasData(new
                {
                    UplataID = Guid.Parse("8D452221-F73E-4E35-BA7C-3FDD0D08BE70"),
                    Iznos = "200",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "3121-424324523-444",
                    BrojRacuna = "155-228523852256500-25",
                    KursID = Guid.Parse("411C4082-CC5E-4F5F-8946-4086EBCA08D0"),
                    JavnoNadmetanjeID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),

                });

            modelBuilder.Entity<UplataEntity>()
                .HasData(new
                {
                    UplataID = Guid.Parse("5F951CF9-AAF2-45C3-823A-5C8C4C1DEAFF"),
                    Iznos = "100",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "0242-424324523-444",
                    BrojRacuna = "155-228523852256500-25",
                    KursID = Guid.Parse("B06A4284-44E2-46AF-8D74-B79C8B0C6017"),
                    JavnoNadmetanjeID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),


                });
            modelBuilder.Entity<UplataEntity>()
                .HasData(new
                {
                    UplataID = Guid.Parse("1D2ED242-5059-4A1B-AEAB-EEE99404284F"),
                    Iznos = "50",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "3221-424324523-444",
                    BrojRacuna = "115-228523852256500-25",
                    KursID = Guid.Parse("411C4082-CC5E-4F5F-8946-4086EBCA08D0"),
                    JavnoNadmetanjeID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                });
        }
    }
}
