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
        private readonly IConfiguration _configuration;
        public UplataContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UplataEntity> Uplate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("UplataDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UplataEntity>(b =>
            {
                b.ToTable("Uplata").HasKey(x => x.UplataID);
                b.OwnsOne(x => x.Kurs, sb =>
                {
                    sb.Property(x => x.VrednostKursa);
                    sb.Property(x => x.Datum);
                    sb.Property(x => x.Valuta);
                });
            });


            builder.Entity<UplataEntity>()
                .HasData(new
                {
                    UplataID = Guid.Parse("8D452221-F73E-4E35-BA7C-3FDD0D08BE70"),
                    Iznos = "150000",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "3121-424324523-444",
                    BrojRacuna = "155-228523852256500-25"
                });

            builder.Entity<UplataEntity>()
                .HasData(new
                {
                    UplataID = Guid.Parse("5F951CF9-AAF2-45C3-823A-5C8C4C1DEAFF"),
                    Iznos = "200000",
                    Datum = DateTime.Now,
                    SvrhaUplate = "ucesce na licitaciji",
                    PozivNaBroj = "0242-424324523-444",
                    BrojRacuna = "155-228523852256500-25"
                });
        }
    }
}
