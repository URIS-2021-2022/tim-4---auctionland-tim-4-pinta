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
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>().HasData(new
            {
                ZalbaID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Datum_podnosenja_zalbe = DateTime.Parse("1-1-2011"),
                Razlog_zalbe = "razlog",
                Obrazlozenje = "obrazlozenje",
                Datum_rijesenja = DateTime.Parse("5-1-2011"),
                Broj_rijesenja = 23,
                Broj_nadmetanja = 32
            }

                );
            modelBuilder.Entity<Complaint>().HasData(new
            {
                ZalbaID = Guid.Parse("ec2e5d91-de9f-4af0-8fae-d8150e338c51"),
                Datum_podnosenja_zalbe = DateTime.Parse("1-1-2012"),
                Razlog_zalbe = "razlog2",
                Obrazlozenje = "obrazlozenje2",
                Datum_rijesenja = DateTime.Parse("5-1-2012"),
                Broj_rijesenja = 23,
                Broj_nadmetanja = 32
            });

            modelBuilder.Entity<ActionBasedOnComplaint>().HasData(new
            {
                Radnja_na_osnovu_zalbe_ID = Guid.Parse("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                JN_ide_u_krug_sa_novim_uslovima = false,
                JN_ide_u_krug_sa_starim_uslovima = true,
                JN_ne_ide_u_drugi_krug = false
            });

            modelBuilder.Entity<ActionBasedOnComplaint>().HasData(new
            {
                Radnja_na_osnovu_zalbe_ID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                JN_ide_u_krug_sa_novim_uslovima = true,
                JN_ide_u_krug_sa_starim_uslovima = false,
                JN_ne_ide_u_drugi_krug = true
            });

            modelBuilder.Entity<StatusOfComplaint>().HasData(new
            {
                Status_zalbe = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Usvojena = false,
                Odbijena = true,
                Otvorena = false
            });

            modelBuilder.Entity<StatusOfComplaint>().HasData(new
            {
                Status_zalbe = Guid.Parse("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                Usvojena = true,
                Odbijena = false,
                Otvorena = true
            });

            modelBuilder.Entity<TypeOfComplaint>().HasData(new
            {
                Tip_id = Guid.Parse("ec2e5d91-de9f-4af0-8fae-d8150e338c51"),
                Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja",
                Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje",
                Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup"
            });

            modelBuilder.Entity<TypeOfComplaint>().HasData(new
            {
                Tip_id = Guid.Parse("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja1",
                Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje1",
                Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup1"
            });
        }
    }
}
