using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Entities
{
    public class LicnostContext : DbContext
    {
        //private readonly IConfiguration configuration;

        //public LicnostContext(DbContextOptions options, IConfiguration configuration) : base(options)
        //{
        //    this.configuration = configuration;
        //}

        public LicnostContext(DbContextOptions<LicnostContext> options) : base(options)
        {

        }

        public DbSet<LicnostEntity> Licnosti { get; set; }
        public DbSet<Komisija> Komisije { get; set; }
        public DbSet<ClanKomisije> ClanoviKomisije { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("LicnostDB"));
        //}



        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LicnostEntity>()
                .HasData(new
                {
                    LicnostId = Guid.Parse("E91B29CC-79A5-4DE8-8030-77DF6E514DEF"),
                    LicnostIme = "Simona",
                    LicnostPrezime = "Bolehradsky",
                    LicnostFunkcija = "IT"
                });

            builder.Entity<LicnostEntity>()
                .HasData(new
                {
                    LicnostId = Guid.Parse("218C05C6-5066-4354-9568-B263AB11713B"),
                    LicnostIme = "Dajana",
                    LicnostPrezime = "Jelic",
                    LicnostFunkcija = "Direktor"
                });

            builder.Entity<Komisija>()
               .HasData(new
               {
                    KomisijaId= Guid.Parse("3540BF55-427A-4892-91D6-633D683EF0ED"),

                     LicnostId= Guid.Parse("E91B29CC-79A5-4DE8-8030-77DF6E514DEF")

               });

            builder.Entity<ClanKomisije>()
                .HasData(new
                {
                   ClanKomisijeId= Guid.Parse("049C45E5-8873-49C2-8275-0B63293F15E7"),

                    LicnostId = Guid.Parse("218C05C6-5066-4354-9568-B263AB11713B"),
                    KomisijaId= Guid.Parse("3540BF55-427A-4892-91D6-633D683EF0ED")

                });
        }
    }
}
