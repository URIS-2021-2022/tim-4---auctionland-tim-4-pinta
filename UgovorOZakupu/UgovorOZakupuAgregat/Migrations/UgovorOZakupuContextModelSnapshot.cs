﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Migrations
{
    [DbContext(typeof(UgovorOZakupuContext))]
    partial class UgovorOZakupuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UgovorOZakupuAgregat.Entities.Dokument", b =>
                {
                    b.Property<Guid>("DokumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumDonosenjaDokumenta")
                        .HasColumnType("datetime2");

                    b.Property<string>("ZavodniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DokumentId");

                    b.ToTable("Dokumenti");

                    b.HasData(
                        new
                        {
                            DokumentId = new Guid("d1209104-7358-4c22-9f4f-415203563a25"),
                            Datum = new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumDonosenjaDokumenta = new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ZavodniBroj = "1234a"
                        },
                        new
                        {
                            DokumentId = new Guid("b030fb0d-7f19-4341-9723-cddb3ddd6980"),
                            Datum = new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumDonosenjaDokumenta = new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ZavodniBroj = "123a"
                        });
                });

            modelBuilder.Entity("UgovorOZakupuAgregat.Entities.RokoviDospeca", b =>
                {
                    b.Property<Guid>("RokId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RokDospeca")
                        .HasColumnType("int");

                    b.Property<Guid>("UgovorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RokId");

                    b.HasIndex("UgovorId");

                    b.ToTable("RokoviDospeca");

                    b.HasData(
                        new
                        {
                            RokId = new Guid("234d1ada-07b8-4789-9c87-86b83118fed0"),
                            RokDospeca = 1,
                            UgovorId = new Guid("407c6e21-0765-44e9-a34b-b2c387814e55")
                        },
                        new
                        {
                            RokId = new Guid("5bed6b27-02a9-484b-b3c7-768b604ff77e"),
                            RokDospeca = 2,
                            UgovorId = new Guid("407c6e21-0765-44e9-a34b-b2c387814e55")
                        });
                });

            modelBuilder.Entity("UgovorOZakupuAgregat.Entities.TipGarancije", b =>
                {
                    b.Property<Guid>("TipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipId");

                    b.ToTable("TipoviGarancije");

                    b.HasData(
                        new
                        {
                            TipId = new Guid("e1f134e5-25f9-4b00-8b96-a809d11cd33b"),
                            Naziv = "Jemstvo"
                        },
                        new
                        {
                            TipId = new Guid("234d1ada-07b8-4789-9c87-86b83118fed0"),
                            Naziv = "Bankarska Garancija"
                        });
                });

            modelBuilder.Entity("UgovorOZakupuAgregat.Entities.UgovorOZakupu", b =>
                {
                    b.Property<Guid>("UgovorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumPotpisa")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZavodjenja")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DokumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MestoPotpisivanja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RokZaVracanjeZemljista")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ZavodniBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UgovorId");

                    b.HasIndex("DokumentId");

                    b.HasIndex("TipId");

                    b.ToTable("Ugovori");

                    b.HasData(
                        new
                        {
                            UgovorId = new Guid("407c6e21-0765-44e9-a34b-b2c387814e55"),
                            DatumPotpisa = new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumZavodjenja = new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DokumentId = new Guid("d1209104-7358-4c22-9f4f-415203563a25"),
                            MestoPotpisivanja = "Novi Sad",
                            RokZaVracanjeZemljista = new DateTime(2021, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipId = new Guid("e1f134e5-25f9-4b00-8b96-a809d11cd33b"),
                            ZavodniBroj = "11a"
                        });
                });

            modelBuilder.Entity("UgovorOZakupuAgregat.Entities.RokoviDospeca", b =>
                {
                    b.HasOne("UgovorOZakupuAgregat.Entities.UgovorOZakupu", "UgovorOZakupu")
                        .WithMany()
                        .HasForeignKey("UgovorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UgovorOZakupu");
                });

            modelBuilder.Entity("UgovorOZakupuAgregat.Entities.UgovorOZakupu", b =>
                {
                    b.HasOne("UgovorOZakupuAgregat.Entities.Dokument", "Dokument")
                        .WithMany()
                        .HasForeignKey("DokumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UgovorOZakupuAgregat.Entities.TipGarancije", "TipGarancije")
                        .WithMany()
                        .HasForeignKey("TipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dokument");

                    b.Navigation("TipGarancije");
                });
#pragma warning restore 612, 618
        }
    }
}
