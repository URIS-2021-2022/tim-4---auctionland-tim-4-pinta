using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.Entities
{
    public class KatastarskaOpstinaContext : DbContext
    {
        

        public KatastarskaOpstinaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<KatastarskaOpstinaEntity> KatastarskeOpstine { get; set; }


        /// <summary>
        /// Popunjava bazu podataka inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<KatastarskaOpstinaEntity>()
                .HasData(new
                {
                    KatastarskaOpstinaID = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F"),
                    NazivKatastarskeOpstine= "Beocin"

                });
            builder.Entity<KatastarskaOpstinaEntity>()
              .HasData(new
              {
                  KatastarskaOpstinaID = Guid.Parse("177E64AD-2FF0-4A40-9C75-1F9B02FFE1E9"),
                  NazivKatastarskeOpstine = "Beograd"

              });
        }
    }
}
