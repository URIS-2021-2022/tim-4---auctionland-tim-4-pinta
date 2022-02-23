using KupacMikroservis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.Entities
{
    public class KupacContext : DbContext
    {
 

        public KupacContext(DbContextOptions<KupacContext> options) : base(options)
        {

        }

    //    public DbSet<KupacEntity> kupci{ get; set; }
        public DbSet<PravnoLiceEntity> pLica { get; set; }
        public DbSet<FizickoLiceEntity> fLica { get; set; }

        public DbSet<KontaktOsobaEntity> kOsobe { get; set; }

        public DbSet<OvlascenoLiceEntity> oLica { get; set; }
        public DbSet<PrioritetEntity> prioriteti { get; set; }

       

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<PrioritetEntity>()
                .HasData(new
                {
                    PrioritetId = Guid.Parse("1a411c13-a195-1117-8dbd-67596c3974c0"),
                    PrioritetOpis = "Visok"


                });

            builder.Entity<PrioritetEntity>()
               .HasData(new
               {
                   PrioritetId = Guid.Parse("1a411c13-a195-2227-8dbd-67596c3974c0"),
                   PrioritetOpis = "Srednji"


               });

            builder.Entity<PrioritetEntity>()
               .HasData(new
               {
                   PrioritetId = Guid.Parse("1a411c13-a195-3337-8dbd-67596c3974c0"),
                   PrioritetOpis = "Nizak"


               });

            builder.Entity<OvlascenoLiceEntity>()
               .HasData(new
               {
                   OvlascenoLiceId = Guid.Parse("1a411c13-a195-3337-8dbd-11111c3974c0"),
                   Ime = "Petar",
                   Prezime = "Petrosevic",
                   BrojLicnogDokumenta="565423433",
                   BrojTable="54356543",
                   AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907")



               });

            builder.Entity<OvlascenoLiceEntity>()
              .HasData(new
              {
                  OvlascenoLiceId = Guid.Parse("1a411c13-a195-3337-8dbd-22222c3974c0"),
                  Ime = "Luka",
                  Prezime = "Lukovic",
                  BrojLicnogDokumenta = "5653424",
                  BrojTable = "543231313",
                  AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907")



              });

            builder.Entity<KontaktOsobaEntity>()
              .HasData(new
              {
                  KontaktOsobaId = Guid.Parse("1a411c13-a195-3337-8dbd-33333c3974c0"),
                  Ime = "Ana",
                  Prezime = "Ankovic",
                  Funkcija = "fja1",
                  Telefon = "65432351"



              });

            builder.Entity<KontaktOsobaEntity>()
              .HasData(new
              {
                  KontaktOsobaId = Guid.Parse("1a411c13-a195-3337-8dbd-44444c3974c0"),
                  Ime = "Milos",
                  Prezime = "Milosevic",
                  Funkcija = "fja2",
                  Telefon = "5432114"



              });

            builder.Entity<PravnoLiceEntity>()
             .HasData(new
             {
                 KupacId = Guid.Parse("2a411c13-a195-48f7-8dbc-67596c3974c0"),
                 IsFizickoLice = false,
                 Naziv = "NS DOO",
                 BrojTelefona1 = "062665231",
                 BrojTelefona2 = "0615573331",
                 Email = "ivaa@gmail.com",
                 BrojRacuna = "2536565534",
                 ImaZabranu = false,
                 DatumPocetkaZabrane = (DateTime?)null,
                 DuzinaTrajanjaZabraneUGodinama = 0,
                 DatumPrestankaZabrane = (DateTime?)null,
                 Prioritet = Guid.Parse("1a411c13-a195-1117-8dbd-67596c3974c0"),
                 OvlascenoLice = Guid.Parse("1a411c13-a195-3337-8dbd-22222c3974c0"),
                 AdresaID = Guid.Parse("9A8E31D5-5E7B-46E7-80C6-F22E607EE907"),
                 UplataID = Guid.Parse("5F951CF9-AAF2-45C3-823A-5C8C4C1DEAFF"),
                 MaticniBroj = "455643231",
                 Faks = "654322345"
                 



             });

            builder.Entity<PravnoLiceEntity>()
           .HasData(new
           {
               KupacId = Guid.Parse("2a421c13-a195-46f7-8dbd-67596c4974c0"),
               IsFizickoLice = false,
               Naziv = "SN AD",
               BrojTelefona1 = "062635321",
               BrojTelefona2 = "0615535651",
               Email = "mikaa@gmail.com",
               BrojRacuna = "253456533534",
               ImaZabranu = false,
               DatumPocetkaZabrane = (DateTime?)null,
               DuzinaTrajanjaZabraneUGodinama = 0,
               DatumPrestankaZabrane = (DateTime?)null,
               Prioritet = Guid.Parse("1a411c13-a195-1117-8dbd-67596c3974c0"),
               OvlascenoLice = Guid.Parse("1a411c13-a195-3337-8dbd-22222c3974c0"),
               AdresaID = Guid.Parse("9A8E31D5-5E7B-46E7-80C6-F22E607EE907"),
               UplataID = Guid.Parse("5F951CF9-AAF2-45C3-823A-5C8C4C1DEAFF"),
               MaticniBroj = "455643231",
               Faks = "654322345"
               



           });


            builder.Entity<FizickoLiceEntity>()
         .HasData(new
         {
             KupacId = Guid.Parse("1a411c13-a195-48f7-8dbd-67596c3974c0"),
             IsFizickoLice = true,
             Naziv = "Pera Peric",
             BrojTelefona1 = "062665511",
             BrojTelefona2 = "061553311",
             Email = "pera@gmail.com",
             BrojRacuna = "2532431234534",
             ImaZabranu = false,
             DatumPocetkaZabrane = (DateTime?)null,
             DuzinaTrajanjaZabraneUGodinama = 0,
             DatumPrestankaZabrane = (DateTime?)null,
             Prioritet = Guid.Parse("1a411c13-a195-1117-8dbd-67596c3974c0"),
             OvlascenoLice = Guid.Parse("1a411c13-a195-3337-8dbd-22222c3974c0"),
             AdresaID = Guid.Parse("9A8E31D5-5E7B-46E7-80C6-F22E607EE907"),
             UplataID = Guid.Parse("5F951CF9-AAF2-45C3-823A-5C8C4C1DEAFF"),
             JMBG = "6765432484",
             KontaktOsoba = Guid.Parse("1a411c13-a195-3337-8dbd-44444c3974c0")
            



         });

            builder.Entity<FizickoLiceEntity>()
         .HasData(new
         {
             KupacId = Guid.Parse("2a411c13-a195-48f7-8dbd-67596c3974c0"),
             IsFizickoLice = true,
             Naziv = "Jova Jovic",
             BrojTelefona1 = "062665521",
             BrojTelefona2 = "061553331",
             Email = "jova@gmail.com",
             BrojRacuna = "253425254534",
             ImaZabranu = false,
             DatumPocetkaZabrane = (DateTime?)null,
             DuzinaTrajanjaZabraneUGodinama = 0,
             DatumPrestankaZabrane = (DateTime?)null,
             Prioritet = Guid.Parse("1a411c13-a195-1117-8dbd-67596c3974c0"),
             OvlascenoLice = Guid.Parse("1a411c13-a195-3337-8dbd-22222c3974c0"),
             AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
             UplataID = Guid.Parse("5F951CF9-AAF2-45C3-823A-5C8C4C1DEAFF"),
             JMBG = "7654321234",
             KontaktOsoba= Guid.Parse("1a411c13-a195-3337-8dbd-33333c3974c0")



         });

         

        }
    }
}
