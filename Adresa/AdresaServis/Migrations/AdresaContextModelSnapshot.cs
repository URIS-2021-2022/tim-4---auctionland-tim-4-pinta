﻿// <auto-generated />
using System;
using AdresaServis.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdresaServis.Migrations
{
    [DbContext(typeof(AdresaContext))]
    partial class AdresaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdresaServis.Entities.AdresaEntity", b =>
                {
                    b.Property<Guid>("AdresaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Broj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DrzavaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mesto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostanskiBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ulica")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdresaID");

                    b.HasIndex("DrzavaID");

                    b.ToTable("Adrese");

                    b.HasData(
                        new
                        {
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            Broj = "50",
                            DrzavaID = new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"),
                            Mesto = "Beograd",
                            PostanskiBroj = "11000",
                            Ulica = "Karadjordjeva"
                        },
                        new
                        {
                            AdresaID = new Guid("723123b1-3ab1-4741-9437-c8a1d6ad20da"),
                            Broj = "4",
                            DrzavaID = new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"),
                            Mesto = "Novi Sad",
                            PostanskiBroj = "21000",
                            Ulica = "Strazilovska"
                        },
                        new
                        {
                            AdresaID = new Guid("eacfb448-52fc-40f0-8815-d7ccce300ece"),
                            Broj = "25",
                            DrzavaID = new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"),
                            Mesto = "Subotica",
                            PostanskiBroj = "12000",
                            Ulica = "Radnicka"
                        },
                        new
                        {
                            AdresaID = new Guid("c0ccfc64-7dd0-4144-b95a-ecfe3ebabeee"),
                            Broj = "2",
                            DrzavaID = new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"),
                            Mesto = "Novi Sad",
                            PostanskiBroj = "21000",
                            Ulica = "Fruskogorska"
                        },
                        new
                        {
                            AdresaID = new Guid("7729e3dc-0586-4ae5-8a0f-2b22b0e2253e"),
                            Broj = "12",
                            DrzavaID = new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"),
                            Mesto = "Subotica",
                            PostanskiBroj = "12000",
                            Ulica = "Cara Lazara"
                        });
                });

            modelBuilder.Entity("AdresaServis.Entities.DrzavaEntity", b =>
                {
                    b.Property<Guid>("DrzavaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivDrzave")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DrzavaID");

                    b.ToTable("Drzave");

                    b.HasData(
                        new
                        {
                            DrzavaID = new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"),
                            NazivDrzave = "Srbija"
                        },
                        new
                        {
                            DrzavaID = new Guid("2b7558a6-f3f4-460d-80e0-26e1c037f455"),
                            NazivDrzave = "Crna Gora"
                        },
                        new
                        {
                            DrzavaID = new Guid("3eced2eb-0a79-4711-a2b6-f6152548440b"),
                            NazivDrzave = "Slovenija"
                        },
                        new
                        {
                            DrzavaID = new Guid("8b8b55ff-4109-4d98-890e-7f0d6aa70fda"),
                            NazivDrzave = "Hrvatska"
                        },
                        new
                        {
                            DrzavaID = new Guid("84feab2f-7b67-4e69-92cc-1f682e89f255"),
                            NazivDrzave = "Bugarska"
                        },
                        new
                        {
                            DrzavaID = new Guid("788c3bd6-1145-4322-8237-1ea25e5a81e6"),
                            NazivDrzave = "Makedonija"
                        });
                });

            modelBuilder.Entity("AdresaServis.Entities.AdresaEntity", b =>
                {
                    b.HasOne("AdresaServis.Entities.DrzavaEntity", "Drzava")
                        .WithMany("Adrese")
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drzava");
                });

            modelBuilder.Entity("AdresaServis.Entities.DrzavaEntity", b =>
                {
                    b.Navigation("Adrese");
                });
#pragma warning restore 612, 618
        }
    }
}
