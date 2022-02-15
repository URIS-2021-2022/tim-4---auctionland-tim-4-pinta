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

        public DbSet<UplataModel> Uplate { get; set; }

        ///protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ///{
           /// optionsBuilder.UseSqlServer(configuration.GetConnectionString("UplataDB"));
        ////}

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UplataModel>()
                .HasData(new
                {
                    UplataID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Iznos = "150000",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "3121-424324523-444",
                    KupacID = Guid.Parse("6a411c23-a195-48f7-8dbd-67596c3974c0"),
                    JavnoNadmetanjeID = Guid.Parse("6a411c23-a192-48f7-8dbd-67596c3974c0")
                });

            builder.Entity<UplataModel>()
                .HasData(new
                {
                    UplataID = Guid.Parse("7a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Iznos = "200000",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "0242-424324523-444",
                    KupacID = Guid.Parse("6a411c23-a195-48f7-8dbd-67596c3974c0"),
                    JavnoNadmetanjeID = Guid.Parse("6a411c23-a192-48f7-8dbd-67596c3974c0")
                });
        }
    }
}
