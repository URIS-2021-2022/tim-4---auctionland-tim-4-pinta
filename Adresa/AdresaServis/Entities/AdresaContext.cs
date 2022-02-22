using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Entities
{
    /// <summary>
    /// Context za bazu podataka
    /// </summary>
    public class AdresaContext : DbContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="options"></param>
        public AdresaContext(DbContextOptions<AdresaContext> options) : base(options)
        {

        }

        /// <summary>
        /// DbSet Adrese
        /// </summary>
        public DbSet<AdresaEntity> Adrese { get; set; }

        /// <summary>
        /// DbSet Drzave
        /// </summary>
        public DbSet<DrzavaEntity> Drzave { get; set; }

        /// <summary>
        /// Kreiranje modela
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrzavaEntity>()
                .HasData(new
                {
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654"),
                    NazivDrzave = "Srbija"
                });

            modelBuilder.Entity<DrzavaEntity>()
                .HasData(new
                {
                    DrzavaID = Guid.Parse("2b7558a6-f3f4-460d-80e0-26e1c037f455"),
                    NazivDrzave = "Crna Gora"
                });

            modelBuilder.Entity<DrzavaEntity>()
               .HasData(new
               {
                   DrzavaID = Guid.Parse("3eced2eb-0a79-4711-a2b6-f6152548440b"),
                   NazivDrzave = "Slovenija"
               });

            modelBuilder.Entity<DrzavaEntity>()
               .HasData(new
               {
                   DrzavaID = Guid.Parse("8b8b55ff-4109-4d98-890e-7f0d6aa70fda"),
                   NazivDrzave = "Hrvatska"
               });

            modelBuilder.Entity<DrzavaEntity>()
               .HasData(new
               {
                   DrzavaID = Guid.Parse("84feab2f-7b67-4e69-92cc-1f682e89f255"),
                   NazivDrzave = "Bugarska"
               });

            modelBuilder.Entity<DrzavaEntity>()
               .HasData(new
               {
                   DrzavaID = Guid.Parse("788c3bd6-1145-4322-8237-1ea25e5a81e6"),
                   NazivDrzave = "Makedonija"
               });

            modelBuilder.Entity<AdresaEntity>()
                .HasData(new
                {
                    AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                    Ulica = "Karadjordjeva",
                    Broj = "50",
                    Mesto = "Beograd",
                    PostanskiBroj = "11000",
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
                });

            modelBuilder.Entity<AdresaEntity>()
                .HasData(new
                {
                    AdresaID = Guid.Parse("723123b1-3ab1-4741-9437-c8a1d6ad20da"),
                    Ulica = "Strazilovska",
                    Broj = "4",
                    Mesto = "Novi Sad",
                    PostanskiBroj = "21000",
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
                });

            modelBuilder.Entity<AdresaEntity>()
                .HasData(new
                {
                    AdresaID = Guid.Parse("eacfb448-52fc-40f0-8815-d7ccce300ece"),
                    Ulica = "Radnicka",
                    Broj = "25",
                    Mesto = "Subotica",
                    PostanskiBroj = "12000",
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
                });

            modelBuilder.Entity<AdresaEntity>()
                .HasData(new
                {
                    AdresaID = Guid.Parse("c0ccfc64-7dd0-4144-b95a-ecfe3ebabeee"),
                    Ulica = "Fruskogorska",
                    Broj = "2",
                    Mesto = "Novi Sad",
                    PostanskiBroj = "21000",
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
                });

            modelBuilder.Entity<AdresaEntity>()
               .HasData(new
               {
                   AdresaID = Guid.Parse("7729e3dc-0586-4ae5-8a0f-2b22b0e2253e"),
                   Ulica = "Cara Lazara",
                   Broj = "12",
                   Mesto = "Subotica",
                   PostanskiBroj = "12000",
                   DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
               });
        }
    }
}
