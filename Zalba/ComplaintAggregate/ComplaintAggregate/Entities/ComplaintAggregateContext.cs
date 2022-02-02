using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintAggregate.Entities
{
    public class ComplaintAggregateContext :DbContext
    {
        public ComplaintAggregateContext(DbContextOptions<ComplaintAggregateContext> options): base(options)
        {

        }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ActionBasedOnComplaint> Actions { get; set; }
        public DbSet<StatusOfComplaint> Status { get; set; }
        public DbSet<TypeOfComplaint> Types { get; set; }
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Complaint>().HasData(new
            {
                ZalbaID = Guid.NewGuid(),
                Datum_podnosenja_zalbe = DateTime.Parse("1-1-2011"),
                Razlog_zalbe = "razlog",
                Obrazlozenje = "obrazlozenje",
                Datum_rijesenja = DateTime.Parse("5-1-2011"),
                Broj_rijesenja = 23,
                Broj_nadmetanja = 32
            }

                );
            builder.Entity<Complaint>().HasData(new
            {
                ZalbaID = Guid.NewGuid(),
                Datum_podnosenja_zalbe = DateTime.Parse("1-1-2012"),
                Razlog_zalbe = "razlog2",
                Obrazlozenje = "obrazlozenje2",
                Datum_rijesenja = DateTime.Parse("5-1-2012"),
                Broj_rijesenja = 23,
                Broj_nadmetanja = 32
            });

            builder.Entity<ActionBasedOnComplaint>().HasData(new
            {
                Radnja_na_osnovu_zalbe_ID = Guid.NewGuid(),
                JN_ide_u_krug_sa_novim_uslovima = false,
                JN_ide_u_krug_sa_starim_uslovima = true,
                JN_ne_ide_u_drugi_krug = false
            });

            builder.Entity<ActionBasedOnComplaint>().HasData(new
            {
                Radnja_na_osnovu_zalbe_ID = Guid.NewGuid(),
                JN_ide_u_krug_sa_novim_uslovima = true,
                JN_ide_u_krug_sa_starim_uslovima = false,
                JN_ne_ide_u_drugi_krug = true
            });

            builder.Entity<StatusOfComplaint>().HasData(new
            {
                Status_zalbe = Guid.NewGuid(),
                Usvojena = false,
                Odbijena = true,
                Otvorena = false
            });

            builder.Entity<StatusOfComplaint>().HasData(new
            {
                Status_zalbe = Guid.NewGuid(),
                Usvojena = true,
                Odbijena = false,
                Otvorena = true
            });

            builder.Entity<TypeOfComplaint>().HasData(new
            {
                Tip_id = Guid.NewGuid(),
                Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja",
                Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje",
                Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup"
            });

            builder.Entity<TypeOfComplaint>().HasData(new
            {
                Tip_id = Guid.NewGuid(),
                Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja1",
                Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje1",
                Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup1"
            });
        }
    }
}
