﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uplata.Entities;

namespace Uplata.Migrations
{
    [DbContext(typeof(UplataContext))]
    [Migration("20220220151004_JavnoNadmetanjeKolona")]
    partial class JavnoNadmetanjeKolona
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Uplata.Entities.UplataEntity", b =>
                {
                    b.Property<Guid>("UplataID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Iznos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("JavnoNadmetanjeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PozivNaBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SvrhaUplate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UplataID");

                    b.ToTable("Uplate");

                    b.HasData(
                        new
                        {
                            UplataID = new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                            BrojRacuna = "155-228523852256500-25",
                            Datum = new DateTime(2022, 2, 20, 16, 10, 3, 829, DateTimeKind.Local).AddTicks(1181),
                            Iznos = "150000",
                            PozivNaBroj = "3121-424324523-444",
                            SvrhaUplate = "ucesce na licitaciji"
                        },
                        new
                        {
                            UplataID = new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                            BrojRacuna = "155-228523852256500-25",
                            Datum = new DateTime(2022, 2, 20, 16, 10, 3, 831, DateTimeKind.Local).AddTicks(4387),
                            Iznos = "200000",
                            PozivNaBroj = "0242-424324523-444",
                            SvrhaUplate = "ucesce na licitaciji"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}