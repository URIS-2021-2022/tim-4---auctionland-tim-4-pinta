using Korisnik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Entities
{
    public class KorisnikContext : DbContext
    {

        private readonly IConfiguration configuration;

        public KorisnikContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<KorisnikModel> KorisnikModels { get; set; }

        public DbSet<TokenTime> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("KorisnikDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KorisnikModel>()
                .HasData(new
                {
                    
                    KorisnikId = 2,
                    Ime = "Petar",
                    Prezime = "Petrović",
                    KorisnickoIme = "IT1/2020",
                    Lozinka = "1",
                    Salt = "1",
                    TipKorisnika = "administrator"
                });

            modelBuilder.Entity<KorisnikModel>()
                .HasData(new
                {

                    KorisnikId = 3,
                    Ime = "Marko",
                    Prezime = "Marković",
                    KorisnickoIme = "IT2/2019",
                    Lozinka ="1",
                    Salt = "1",
                    TipKorisnika = "licitant"
                });

            modelBuilder.Entity<TokenTime>()
                .HasData(new
                {
                    tokenId = 1,
                    token = "token",
                    korisnikId = 3,
                    time = DateTime.Parse("2000-10-10 10:10:10")
                } );
        }
    }
}
