﻿// <auto-generated />
using System;
using Korisnik.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Korisnik.Migrations
{
    [DbContext(typeof(KorisnikContext))]
    [Migration("20220221162253_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Korisnik.Models.KorisnikModel", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipKorisnika")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikId");

                    b.ToTable("KorisnikModels");

                    b.HasData(
                        new
                        {
                            KorisnikId = 2,
                            Ime = "Petar",
                            KorisnickoIme = "IT1/2020",
                            Lozinka = "1",
                            Prezime = "Petrović",
                            Salt = "1",
                            TipKorisnika = "administrator"
                        },
                        new
                        {
                            KorisnikId = 3,
                            Ime = "Marko",
                            KorisnickoIme = "IT2/2019",
                            Lozinka = "1",
                            Prezime = "Marković",
                            Salt = "1",
                            TipKorisnika = "licitant"
                        });
                });

            modelBuilder.Entity("Korisnik.Models.TokenTime", b =>
                {
                    b.Property<int>("tokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("korisnikId")
                        .HasColumnType("int");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.Property<string>("token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tokenId");

                    b.ToTable("Tokens");

                    b.HasData(
                        new
                        {
                            tokenId = 1,
                            korisnikId = 3,
                            time = new DateTime(2000, 10, 10, 10, 10, 10, 0, DateTimeKind.Unspecified),
                            token = "token"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}