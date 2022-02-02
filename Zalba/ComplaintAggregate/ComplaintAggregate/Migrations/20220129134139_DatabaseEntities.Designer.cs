﻿// <auto-generated />
using System;
using ComplaintAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComplaintAggregate.Migrations
{
    [DbContext(typeof(ComplaintAggregateContext))]
    [Migration("20220129134139_DatabaseEntities")]
    partial class DatabaseEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComplaintAggregate.Entities.ActionBasedOnComplaint", b =>
                {
                    b.Property<Guid>("Radnja_na_osnovu_zalbe_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("JN_ide_u_krug_sa_novim_uslovima")
                        .HasColumnType("bit");

                    b.Property<bool>("JN_ide_u_krug_sa_starim_uslovima")
                        .HasColumnType("bit");

                    b.Property<bool>("JN_ne_ide_u_drugi_krug")
                        .HasColumnType("bit");

                    b.HasKey("Radnja_na_osnovu_zalbe_ID");

                    b.ToTable("Actions");

                    b.HasData(
                        new
                        {
                            Radnja_na_osnovu_zalbe_ID = new Guid("07dea195-00ae-486a-8928-42921aec68bd"),
                            JN_ide_u_krug_sa_novim_uslovima = false,
                            JN_ide_u_krug_sa_starim_uslovima = true,
                            JN_ne_ide_u_drugi_krug = false
                        },
                        new
                        {
                            Radnja_na_osnovu_zalbe_ID = new Guid("820965da-e3fe-4ef2-bed4-7b131101f771"),
                            JN_ide_u_krug_sa_novim_uslovima = true,
                            JN_ide_u_krug_sa_starim_uslovima = false,
                            JN_ne_ide_u_drugi_krug = true
                        });
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.Complaint", b =>
                {
                    b.Property<Guid>("ZalbaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Broj_nadmetanja")
                        .HasColumnType("int");

                    b.Property<int>("Broj_rijesenja")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum_podnosenja_zalbe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Datum_rijesenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Obrazlozenje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Razlog_zalbe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZalbaID");

                    b.ToTable("Complaints");

                    b.HasData(
                        new
                        {
                            ZalbaID = new Guid("a9e48369-5661-41b7-bdf2-6059a0224554"),
                            Broj_nadmetanja = 32,
                            Broj_rijesenja = 23,
                            Datum_podnosenja_zalbe = new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Datum_rijesenja = new DateTime(2011, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Obrazlozenje = "obrazlozenje",
                            Razlog_zalbe = "razlog"
                        },
                        new
                        {
                            ZalbaID = new Guid("eff33630-25b3-4e9e-a3ee-61e21226bb9c"),
                            Broj_nadmetanja = 32,
                            Broj_rijesenja = 23,
                            Datum_podnosenja_zalbe = new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Datum_rijesenja = new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Obrazlozenje = "obrazlozenje2",
                            Razlog_zalbe = "razlog2"
                        });
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.StatusOfComplaint", b =>
                {
                    b.Property<Guid>("Status_zalbe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Odbijena")
                        .HasColumnType("bit");

                    b.Property<bool>("Otvorena")
                        .HasColumnType("bit");

                    b.Property<bool>("Usvojena")
                        .HasColumnType("bit");

                    b.HasKey("Status_zalbe");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Status_zalbe = new Guid("5cf8c011-349e-46dd-a668-cdc180a089a4"),
                            Odbijena = true,
                            Otvorena = false,
                            Usvojena = false
                        },
                        new
                        {
                            Status_zalbe = new Guid("698c3c91-33fc-4720-993f-f09bc1262814"),
                            Odbijena = false,
                            Otvorena = true,
                            Usvojena = true
                        });
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.TypeOfComplaint", b =>
                {
                    b.Property<Guid>("Tip_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Zalba_na_odluku_o_davanju_na_koriscenje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zalba_na_odluku_o_davanju_na_zakup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zalba_na_tok_javnog_nadmetanja")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Tip_id");

                    b.ToTable("Types");

                    b.HasData(
                        new
                        {
                            Tip_id = new Guid("27b9e209-ca15-4f71-b815-a060a6c96c2b"),
                            Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje",
                            Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup",
                            Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja"
                        },
                        new
                        {
                            Tip_id = new Guid("eebf3f9d-4457-41b4-a8aa-344f265f0109"),
                            Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje1",
                            Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup1",
                            Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja1"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
