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
    [Migration("20220206231929_FinalMigration")]
    partial class FinalMigration
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
                            Radnja_na_osnovu_zalbe_ID = new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                            JN_ide_u_krug_sa_novim_uslovima = false,
                            JN_ide_u_krug_sa_starim_uslovima = true,
                            JN_ne_ide_u_drugi_krug = false
                        },
                        new
                        {
                            Radnja_na_osnovu_zalbe_ID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
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

                    b.Property<Guid>("Radnja_na_osnovu_zalbe_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Razlog_zalbe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Status_zalbe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Tip_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ZalbaID");

                    b.HasIndex("Radnja_na_osnovu_zalbe_ID");

                    b.HasIndex("Status_zalbe");

                    b.HasIndex("Tip_id");

                    b.ToTable("Complaints");

                    b.HasData(
                        new
                        {
                            ZalbaID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Broj_nadmetanja = 32,
                            Broj_rijesenja = 23,
                            Datum_podnosenja_zalbe = new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Datum_rijesenja = new DateTime(2011, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Obrazlozenje = "obrazlozenje",
                            Radnja_na_osnovu_zalbe_ID = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Razlog_zalbe = "razlog",
                            Status_zalbe = new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                            Tip_id = new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd")
                        },
                        new
                        {
                            ZalbaID = new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51"),
                            Broj_nadmetanja = 32,
                            Broj_rijesenja = 23,
                            Datum_podnosenja_zalbe = new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Datum_rijesenja = new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Obrazlozenje = "obrazlozenje2",
                            Radnja_na_osnovu_zalbe_ID = new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                            Razlog_zalbe = "razlog2",
                            Status_zalbe = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Tip_id = new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51")
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
                            Status_zalbe = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                            Odbijena = true,
                            Otvorena = false,
                            Usvojena = false
                        },
                        new
                        {
                            Status_zalbe = new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
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
                            Tip_id = new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51"),
                            Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje",
                            Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup",
                            Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja"
                        },
                        new
                        {
                            Tip_id = new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"),
                            Zalba_na_odluku_o_davanju_na_koriscenje = "davanje na koriscenje1",
                            Zalba_na_odluku_o_davanju_na_zakup = "davanje na zakup1",
                            Zalba_na_tok_javnog_nadmetanja = "tok javnog nadmetanja1"
                        });
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.Complaint", b =>
                {
                    b.HasOne("ComplaintAggregate.Entities.ActionBasedOnComplaint", "ActionBasedOnComplaint")
                        .WithMany("Complaints")
                        .HasForeignKey("Radnja_na_osnovu_zalbe_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintAggregate.Entities.StatusOfComplaint", "StatusOfComplaint")
                        .WithMany("Complaints")
                        .HasForeignKey("Status_zalbe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintAggregate.Entities.TypeOfComplaint", "TypeOfComplaint")
                        .WithMany("Complaints")
                        .HasForeignKey("Tip_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActionBasedOnComplaint");

                    b.Navigation("StatusOfComplaint");

                    b.Navigation("TypeOfComplaint");
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.ActionBasedOnComplaint", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.StatusOfComplaint", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("ComplaintAggregate.Entities.TypeOfComplaint", b =>
                {
                    b.Navigation("Complaints");
                });
#pragma warning restore 612, 618
        }
    }
}