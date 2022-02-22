using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija.Entities
{

    /// <summary>
    /// Context za bazu podataka
    /// </summary>
    public class LicitacijaContext : DbContext
    {
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="options"></param>
        public LicitacijaContext(DbContextOptions options) : base(options)
        {
           
        }
        /// <summary>
        /// DbSet Licitacije
        /// </summary>
        public DbSet<LicitacijaEntity> Licitacije { get; set; }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LicitacijaEntity>()
                .HasData(new
                {
                    LicitacijaID= Guid.Parse("8D452221-F73E-4E35-BA7C-3FDD0D08BE70"),
                    Broj = 1,
                    Godina = 2002,
                    Datum = DateTime.Now,
                    Ogranicenje = 100,
                    Rok = DateTime.Now,
                    DokFizickog = "Dokument1",
                    DokPravnog = "Dokument1",
                    KorakCene = 100,
                    JavnoNadmetanjeID = Guid.Parse("8D452221-F73E-4E35-BA7C-3FDD0D08BE70"),
                    KupacID = Guid.Parse("1a411c13-a195-48f7-8dbd-67596c3974c0"),

                });
            builder.Entity<LicitacijaEntity>()
                .HasData(new
                {
                    LicitacijaID= Guid.Parse("4879EC40-DF11-457C-9BD1-07CD2B4EC7CD"),
                    Broj = 2,
                    Godina = 2020,
                    Datum = DateTime.Now,
                    Ogranicenje = 200,
                    Rok = DateTime.Now,
                    DokFizickog = "Dokument2",
                    DokPravnog = "Dokument2",
                    KorakCene = 200,
                    JavnoNadmetanjeID = Guid.Parse("8D452221-F73E-4E35-BA7C-3FDD0D08BE70"),
                    KupacID = Guid.Parse("1a411c13-a195-48f7-8dbd-67596c3974c0"),

                });
        }
    }
}
