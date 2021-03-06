using Microsoft.EntityFrameworkCore;
using System;

namespace UgovorOZakupuAgregat.Entities
{
    public class UgovorOZakupuContext : DbContext
    {
        public UgovorOZakupuContext(DbContextOptions<UgovorOZakupuContext> options) : base(options)
        {
        }

        public DbSet<UgovorOZakupu> Ugovori { get; set; }
        public DbSet<Dokument> Dokumenti { get; set; }
        public DbSet<TipGarancije> TipoviGarancije { get; set; }
        public DbSet<RokoviDospeca> RokoviDospeca { get; set; }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dokument>()
               .HasData(new
               {
                   DokumentId = Guid.Parse("D1209104-7358-4C22-9F4F-415203563A25"),
                   ZavodniBroj = "1234a",
                   Datum = DateTime.Parse("25-01-2021"),
                   DatumDonosenjaDokumenta = DateTime.Parse("25-01-2021")
               });

            modelBuilder.Entity<Dokument>()
               .HasData(new
               {
                   DokumentId = Guid.Parse("B030FB0D-7F19-4341-9723-CDDB3DDD6980"),
                   ZavodniBroj = "123a",
                   Datum = DateTime.Parse("24-01-2021"),
                   DatumDonosenjaDokumenta = DateTime.Parse("24-01-2021")
               });

            modelBuilder.Entity<TipGarancije>()
                .HasData(new
                {
                    TipId = Guid.Parse("E1F134E5-25F9-4B00-8B96-A809D11CD33B"),
                    Naziv = "Jemstvo"
                });

            modelBuilder.Entity<TipGarancije>()
                .HasData(new
                {
                    TipId = Guid.Parse("234D1ADA-07B8-4789-9C87-86B83118FED0"),
                    Naziv = "Bankarska Garancija"
                });

            modelBuilder.Entity<UgovorOZakupu>()
                .HasData(new
                {
                    UgovorId = Guid.Parse("407C6E21-0765-44E9-A34B-B2C387814E55"),
                    DokumentId = Guid.Parse("D1209104-7358-4C22-9F4F-415203563A25"),
                    TipId = Guid.Parse("E1F134E5-25F9-4B00-8B96-A809D11CD33B"),
                    // RokId= Guid.Parse("234D1ADA-07B8-4789-9C87-86B83118FED0"),
                    ZavodniBroj = "11a",
                    DatumZavodjenja = DateTime.Parse("24-01-2021"),
                    RokZaVracanjeZemljista = DateTime.Parse("24-05-2021"),
                    MestoPotpisivanja = "Novi Sad",
                    DatumPotpisa = DateTime.Parse("25-01-2021"),
                    LicnostId = Guid.Parse("E91B29CC-79A5-4DE8-8030-77DF6E514DEF"),
                    KupacId = Guid.Parse("1a411c13-a195-48f7-8dbd-67596c3974c0"),
                    JavnoNadmetanjeId = Guid.Parse("3BD80C2A-C790-402F-B214-E3EBBC29D89F")
                });

            modelBuilder.Entity<RokoviDospeca>()
               .HasData(new
               {
                   RokId = Guid.Parse("234D1ADA-07B8-4789-9C87-86B83118FED0"),
                   UgovorId = Guid.Parse("407C6E21-0765-44E9-A34B-B2C387814E55"),
                   RokDospeca = 1
               });

            modelBuilder.Entity<RokoviDospeca>()
           .HasData(new
           {
               RokId = Guid.Parse("5BED6B27-02A9-484B-B3C7-768B604FF77E"),
               UgovorId = Guid.Parse("407C6E21-0765-44E9-A34B-B2C387814E55"),
               RokDospeca = 2
           });
        }
    }
}