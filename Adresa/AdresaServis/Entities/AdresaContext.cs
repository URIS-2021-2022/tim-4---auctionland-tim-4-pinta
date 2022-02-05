﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Entities
{
    public class AdresaContext : DbContext
    {
        public AdresaContext(DbContextOptions<AdresaContext> options) : base(options)
        {

        }

        public DbSet<AdresaEntity> Adrese { get; set; }

        public DbSet<DrzavaEntity> Drzave { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdresaEntity>()
                .HasData(new
                {
                    AdresaID = Guid.Parse("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                    Ulica = "Karadjordjeva",
                    Broj = "50",
                    Mesto = "Beograd",
                    PostanskiBroj = "11000",
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
                });

            builder.Entity<AdresaEntity>()
                .HasData(new
                {
                    AdresaID = Guid.Parse("723123b1-3ab1-4741-9437-c8a1d6ad20da"),
                    Ulica = "Strazilovska",
                    Broj = "4",
                    Mesto = "Novi Sad",
                    PostanskiBroj = "21000",
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654")
                });

            builder.Entity<DrzavaEntity>()
                .HasData(new
                {
                    DrzavaID = Guid.Parse("fd5e46de-290f-4844-a004-4a94ae24f654"),
                    NazivDrzave = "Srbija"
                });

            builder.Entity<AdresaEntity>()
                .HasData(new
                {
                    DrzavaID = Guid.Parse("2b7558a6-f3f4-460d-80e0-26e1c037f455"),
                    NazivDrzave = "Crna Gora"
                });
        }
    }
}