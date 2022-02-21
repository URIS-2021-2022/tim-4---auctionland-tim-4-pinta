﻿// <auto-generated />
using System;
using KupacMikroservis.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KupacMikroservis.Migrations
{
    [DbContext(typeof(KupacContext))]
    partial class KupacContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KupacMikroservis.Models.FizickoLiceEntity", b =>
                {
                    b.Property<Guid>("KupacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdresaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DatumPocetkaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumPrestankaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<int>("DuzinaTrajanjaZabraneUGodinama")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ImaZabranu")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFizickoLice")
                        .HasColumnType("bit");

                    b.Property<string>("JMBG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KontaktOsoba")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OvlascenoLice")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Prioritet")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UplataID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KupacId");

                    b.ToTable("fLica");

                    b.HasData(
                        new
                        {
                            KupacId = new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"),
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            BrojRacuna = "2532431234534",
                            BrojTelefona1 = "062665511",
                            BrojTelefona2 = "061553311",
                            DuzinaTrajanjaZabraneUGodinama = 0,
                            Email = "pera@gmail.com",
                            ImaZabranu = false,
                            IsFizickoLice = true,
                            JMBG = "6765432484",
                            KontaktOsoba = new Guid("1a411c13-a195-3337-8dbd-44444c3974c0"),
                            Naziv = "Pera Peric",
                            OvlascenoLice = new Guid("1a411c13-a185-48f7-8dbd-67596c3974c8"),
                            Prioritet = new Guid("1a411c13-a195-1117-8dbd-67596c3974c0"),
                            UplataID = new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70")
                        },
                        new
                        {
                            KupacId = new Guid("2a411c13-a195-48f7-8dbd-67596c3974c0"),
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            BrojRacuna = "253425254534",
                            BrojTelefona1 = "062665521",
                            BrojTelefona2 = "061553331",
                            DuzinaTrajanjaZabraneUGodinama = 0,
                            Email = "jova@gmail.com",
                            ImaZabranu = false,
                            IsFizickoLice = true,
                            JMBG = "7654321234",
                            KontaktOsoba = new Guid("1a411c13-a195-3337-8dbd-33333c3974c0"),
                            Naziv = "Jova Jovic",
                            OvlascenoLice = new Guid("1a411c13-a185-48f7-8dbd-67596c3975c8"),
                            Prioritet = new Guid("1a411c13-a195-1117-8dbd-67596c3974c0"),
                            UplataID = new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70")
                        });
                });

            modelBuilder.Entity("KupacMikroservis.Models.KontaktOsobaEntity", b =>
                {
                    b.Property<Guid>("KontaktOsobaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Funkcija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KontaktOsobaId");

                    b.ToTable("kOsobe");

                    b.HasData(
                        new
                        {
                            KontaktOsobaId = new Guid("1a411c13-a195-3337-8dbd-33333c3974c0"),
                            Funkcija = "fja1",
                            Ime = "Ana",
                            Prezime = "Ankovic",
                            Telefon = "65432351"
                        },
                        new
                        {
                            KontaktOsobaId = new Guid("1a411c13-a195-3337-8dbd-44444c3974c0"),
                            Funkcija = "fja2",
                            Ime = "Milos",
                            Prezime = "Milosevic",
                            Telefon = "5432114"
                        });
                });

            modelBuilder.Entity("KupacMikroservis.Models.OvlascenoLiceEntity", b =>
                {
                    b.Property<Guid>("OvlascenoLiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdresaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojLicnogDokumenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OvlascenoLiceId");

                    b.ToTable("oLica");

                    b.HasData(
                        new
                        {
                            OvlascenoLiceId = new Guid("1a411c13-a195-3337-8dbd-11111c3974c0"),
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            BrojLicnogDokumenta = "565423433",
                            BrojTable = "54356543",
                            Ime = "Petar",
                            Prezime = "Petrosevic"
                        },
                        new
                        {
                            OvlascenoLiceId = new Guid("1a411c13-a195-3337-8dbd-22222c3974c0"),
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            BrojLicnogDokumenta = "5653424",
                            BrojTable = "543231313",
                            Ime = "Luka",
                            Prezime = "Lukovic"
                        });
                });

            modelBuilder.Entity("KupacMikroservis.Models.PravnoLiceEntity", b =>
                {
                    b.Property<Guid>("KupacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdresaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DatumPocetkaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumPrestankaZabrane")
                        .HasColumnType("datetime2");

                    b.Property<int>("DuzinaTrajanjaZabraneUGodinama")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ImaZabranu")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFizickoLice")
                        .HasColumnType("bit");

                    b.Property<string>("MaticniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OvlascenoLice")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Prioritet")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UplataID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KupacId");

                    b.ToTable("pLica");

                    b.HasData(
                        new
                        {
                            KupacId = new Guid("2a411c13-a195-48f7-8dbc-67596c3974c0"),
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            BrojRacuna = "2536565534",
                            BrojTelefona1 = "062665231",
                            BrojTelefona2 = "0615573331",
                            DuzinaTrajanjaZabraneUGodinama = 0,
                            Email = "ivaa@gmail.com",
                            Faks = "654322345",
                            ImaZabranu = false,
                            IsFizickoLice = false,
                            MaticniBroj = "455643231",
                            Naziv = "NS DOO",
                            OvlascenoLice = new Guid("1a411c13-a185-48f7-8dbd-67596c3975c8"),
                            Prioritet = new Guid("1a411c13-a195-1117-8dbd-67596c3974c0"),
                            UplataID = new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70")
                        },
                        new
                        {
                            KupacId = new Guid("2a421c13-a195-46f7-8dbd-67596c4974c0"),
                            AdresaID = new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"),
                            BrojRacuna = "253456533534",
                            BrojTelefona1 = "062635321",
                            BrojTelefona2 = "0615535651",
                            DuzinaTrajanjaZabraneUGodinama = 0,
                            Email = "mikaa@gmail.com",
                            Faks = "654322345",
                            ImaZabranu = false,
                            IsFizickoLice = false,
                            MaticniBroj = "455643231",
                            Naziv = "SN AD",
                            OvlascenoLice = new Guid("1a411c13-a185-48f7-8dbd-67596c3975c8"),
                            Prioritet = new Guid("1a411c13-a195-1117-8dbd-67596c3974c0"),
                            UplataID = new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70")
                        });
                });

            modelBuilder.Entity("KupacMikroservis.Models.PrioritetEntity", b =>
                {
                    b.Property<Guid>("PrioritetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PrioritetOpis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrioritetId");

                    b.ToTable("prioriteti");

                    b.HasData(
                        new
                        {
                            PrioritetId = new Guid("1a411c13-a195-1117-8dbd-67596c3974c0"),
                            PrioritetOpis = "Visok"
                        },
                        new
                        {
                            PrioritetId = new Guid("1a411c13-a195-2227-8dbd-67596c3974c0"),
                            PrioritetOpis = "Srednji"
                        },
                        new
                        {
                            PrioritetId = new Guid("1a411c13-a195-3337-8dbd-67596c3974c0"),
                            PrioritetOpis = "Nizak"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
