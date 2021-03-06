// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parcela.Entities;

namespace Parcela.Migrations
{
    [DbContext(typeof(ParcelaContext))]
    partial class ParcelaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parcela.Entities.DeoParceleEntity", b =>
                {
                    b.Property<Guid>("DeoParceleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParcelaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PovrsinaDelaParcele")
                        .HasColumnType("int");

                    b.Property<int>("RedniBroj")
                        .HasColumnType("int");

                    b.HasKey("DeoParceleID");

                    b.HasIndex("ParcelaID");

                    b.ToTable("DeloviParcela");

                    b.HasData(
                        new
                        {
                            DeoParceleID = new Guid("cae99a88-c6ee-4f4c-a463-419ac8fc1b85"),
                            ParcelaID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            PovrsinaDelaParcele = 2000,
                            RedniBroj = 1
                        },
                        new
                        {
                            DeoParceleID = new Guid("c1df8ec6-dcae-4b27-bb33-7539fb6125c0"),
                            ParcelaID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            PovrsinaDelaParcele = 3000,
                            RedniBroj = 1
                        },
                        new
                        {
                            DeoParceleID = new Guid("910876c8-ab40-4a29-a047-f9913ebaefb8"),
                            ParcelaID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            PovrsinaDelaParcele = 4000,
                            RedniBroj = 3
                        },
                        new
                        {
                            DeoParceleID = new Guid("2884b2b0-302c-4eac-847c-65e4c356132b"),
                            ParcelaID = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            PovrsinaDelaParcele = 2000,
                            RedniBroj = 1
                        });
                });

            modelBuilder.Entity("Parcela.Entities.KlasaEntity", b =>
                {
                    b.Property<Guid>("KlasaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("KlasaOznaka")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KlasaID");

                    b.ToTable("Klase");

                    b.HasData(
                        new
                        {
                            KlasaID = new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                            KlasaOznaka = "I"
                        },
                        new
                        {
                            KlasaID = new Guid("18227841-6ba9-4509-b8fa-faa8f6699b3b"),
                            KlasaOznaka = "II"
                        },
                        new
                        {
                            KlasaID = new Guid("b417f2f5-5b3a-4856-a140-49a361d4cfd5"),
                            KlasaOznaka = "III"
                        },
                        new
                        {
                            KlasaID = new Guid("1d6f312f-73e5-4a57-9dd5-ba31f08bb967"),
                            KlasaOznaka = "IV"
                        },
                        new
                        {
                            KlasaID = new Guid("560475bd-3701-405b-9be8-e89768ce3eb5"),
                            KlasaOznaka = "V"
                        },
                        new
                        {
                            KlasaID = new Guid("d2c9a6ad-8c0d-44d2-8b56-2c4eada4ff99"),
                            KlasaOznaka = "VI"
                        },
                        new
                        {
                            KlasaID = new Guid("51c6ad49-5b78-424d-9cf5-259cb7d9e0e0"),
                            KlasaOznaka = "VII"
                        },
                        new
                        {
                            KlasaID = new Guid("9a011828-2b22-4666-b300-fe98e2c94d9a"),
                            KlasaOznaka = "VIII"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.KulturaEntity", b =>
                {
                    b.Property<Guid>("KulturaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("KulturaNaziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KulturaID");

                    b.ToTable("Kulture");

                    b.HasData(
                        new
                        {
                            KulturaID = new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                            KulturaNaziv = "Njive"
                        },
                        new
                        {
                            KulturaID = new Guid("86f5706f-737b-4b20-beed-531aa64326cb"),
                            KulturaNaziv = "Vrtovi"
                        },
                        new
                        {
                            KulturaID = new Guid("36840f4a-91c7-48b2-a85e-c2f285db0a56"),
                            KulturaNaziv = "Vocnjaci"
                        },
                        new
                        {
                            KulturaID = new Guid("04e29d95-7330-4d42-a10a-08556d478a46"),
                            KulturaNaziv = "Vinogradi"
                        },
                        new
                        {
                            KulturaID = new Guid("67db5b31-6cb1-4bd0-a6a9-52702b06ced4"),
                            KulturaNaziv = "Livade"
                        },
                        new
                        {
                            KulturaID = new Guid("e7977a9e-74c7-4f4b-91b7-57fc03159456"),
                            KulturaNaziv = "Pasnjaci"
                        },
                        new
                        {
                            KulturaID = new Guid("cb674d70-bd30-4ed5-bcc0-b5db489bfbe7"),
                            KulturaNaziv = "Sume"
                        },
                        new
                        {
                            KulturaID = new Guid("5f4a3a1e-3406-4991-abd4-0f095b59ac84"),
                            KulturaNaziv = "Trstici-mocvare"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.OblikSvojineEntity", b =>
                {
                    b.Property<Guid>("OblikSvojineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OblikSvojineNaziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OblikSvojineID");

                    b.ToTable("ObliciSvojine");

                    b.HasData(
                        new
                        {
                            OblikSvojineID = new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                            OblikSvojineNaziv = "Privatno"
                        },
                        new
                        {
                            OblikSvojineID = new Guid("91a1f792-bc28-4f6e-bdda-cb577d7858fe"),
                            OblikSvojineNaziv = "Drzavna svojina RS"
                        },
                        new
                        {
                            OblikSvojineID = new Guid("8cd557cd-b1ce-4a6e-8491-ddc80310d1e7"),
                            OblikSvojineNaziv = "Drzavna svojina"
                        },
                        new
                        {
                            OblikSvojineID = new Guid("11474cc8-1ac3-47b5-87f0-c7de7f29f024"),
                            OblikSvojineNaziv = "Drustvena svojina"
                        },
                        new
                        {
                            OblikSvojineID = new Guid("64bda426-0a91-44fa-8da9-93cf24cc93ae"),
                            OblikSvojineNaziv = "Zadruzna svojina"
                        },
                        new
                        {
                            OblikSvojineID = new Guid("2d83379c-87e6-45a1-9f00-321b820062fc"),
                            OblikSvojineNaziv = "Mesovita svojina"
                        },
                        new
                        {
                            OblikSvojineID = new Guid("085f566f-9900-45e4-800a-d679331b8050"),
                            OblikSvojineNaziv = "Drugi oblici"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.ObradivostEntity", b =>
                {
                    b.Property<Guid>("ObradivostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ObradivostNaziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ObradivostID");

                    b.ToTable("Obradivosti");

                    b.HasData(
                        new
                        {
                            ObradivostID = new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                            ObradivostNaziv = "Obradivo"
                        },
                        new
                        {
                            ObradivostID = new Guid("bf45ffef-1166-44fb-a2e1-67824a6561f2"),
                            ObradivostNaziv = "Ostalo"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.OdvodnjavanjeEntity", b =>
                {
                    b.Property<Guid>("OdvodnjavanjeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OdvodnjavanjeNaziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OdvodnjavanjeID");

                    b.ToTable("Odvodnjavanja");

                    b.HasData(
                        new
                        {
                            OdvodnjavanjeID = new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                            OdvodnjavanjeNaziv = "Povrsinsko"
                        },
                        new
                        {
                            OdvodnjavanjeID = new Guid("a2f44a7b-cdfb-4d69-b651-6d715afe8217"),
                            OdvodnjavanjeNaziv = "Podzemno"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.ParcelaEntity", b =>
                {
                    b.Property<Guid>("ParcelaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojListaNepokretnosti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojParcele")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KatastarskaOpstinaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KlasaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("KlasaStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KulturaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("KulturaStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KupacID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OblikSvojineID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ObradivostID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ObradivostStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OdvodnjavanjeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OdvodnjavanjeStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Povrsina")
                        .HasColumnType("int");

                    b.Property<Guid>("ZasticenaZonaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ZasticenaZonaStvarnoStanje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParcelaID");

                    b.HasIndex("KlasaID");

                    b.HasIndex("KulturaID");

                    b.HasIndex("OblikSvojineID");

                    b.HasIndex("ObradivostID");

                    b.HasIndex("OdvodnjavanjeID");

                    b.HasIndex("ZasticenaZonaID");

                    b.ToTable("Parcele");

                    b.HasData(
                        new
                        {
                            ParcelaID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            BrojListaNepokretnosti = "12",
                            BrojParcele = "111",
                            KatastarskaOpstinaID = new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"),
                            KlasaID = new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                            KlasaStvarnoStanje = "I",
                            KulturaID = new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                            KulturaStvarnoStanje = "Njive",
                            KupacID = new Guid("2a411c13-a195-48f7-8dbc-67596c3974c0"),
                            OblikSvojineID = new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                            ObradivostID = new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                            ObradivostStvarnoStanje = "Obradivo",
                            OdvodnjavanjeID = new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                            OdvodnjavanjeStvarnoStanje = "Povrsinsko",
                            Povrsina = 10000,
                            ZasticenaZonaID = new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                            ZasticenaZonaStvarnoStanje = "1"
                        },
                        new
                        {
                            ParcelaID = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            BrojListaNepokretnosti = "92",
                            BrojParcele = "222",
                            KatastarskaOpstinaID = new Guid("177e64ad-2ff0-4a40-9c75-1f9b02ffe1e9"),
                            KlasaID = new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                            KlasaStvarnoStanje = "II",
                            KulturaID = new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                            KulturaStvarnoStanje = "Vinogradi",
                            KupacID = new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"),
                            OblikSvojineID = new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                            ObradivostID = new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                            ObradivostStvarnoStanje = "Obradivo",
                            OdvodnjavanjeID = new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                            OdvodnjavanjeStvarnoStanje = "Podzemno",
                            Povrsina = 2000,
                            ZasticenaZonaID = new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                            ZasticenaZonaStvarnoStanje = "2"
                        },
                        new
                        {
                            ParcelaID = new Guid("228927ab-e8fd-4d7e-8986-b9c3c4930480"),
                            BrojListaNepokretnosti = "54",
                            BrojParcele = "333",
                            KatastarskaOpstinaID = new Guid("177e64ad-2ff0-4a40-9c75-1f9b02ffe1e9"),
                            KlasaID = new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                            KlasaStvarnoStanje = "III",
                            KulturaID = new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                            KulturaStvarnoStanje = "Vocnjaci",
                            KupacID = new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"),
                            OblikSvojineID = new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                            ObradivostID = new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                            ObradivostStvarnoStanje = "Ostalo",
                            OdvodnjavanjeID = new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                            OdvodnjavanjeStvarnoStanje = "Podzemno",
                            Povrsina = 3000,
                            ZasticenaZonaID = new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                            ZasticenaZonaStvarnoStanje = "3"
                        },
                        new
                        {
                            ParcelaID = new Guid("a913cb6f-9608-474a-88dc-4f38a51315ea"),
                            BrojListaNepokretnosti = "63",
                            BrojParcele = "444",
                            KatastarskaOpstinaID = new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"),
                            KlasaID = new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"),
                            KlasaStvarnoStanje = "I",
                            KulturaID = new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"),
                            KulturaStvarnoStanje = "Njive",
                            KupacID = new Guid("2a411c13-a195-48f7-8dbc-67596c3974c0"),
                            OblikSvojineID = new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"),
                            ObradivostID = new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"),
                            ObradivostStvarnoStanje = "Obradivo",
                            OdvodnjavanjeID = new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"),
                            OdvodnjavanjeStvarnoStanje = "Povrsinsko",
                            Povrsina = 4000,
                            ZasticenaZonaID = new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                            ZasticenaZonaStvarnoStanje = "1"
                        });
                });

            modelBuilder.Entity("Parcela.Entities.ZasticenaZonaEntity", b =>
                {
                    b.Property<Guid>("ZasticenaZonaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ZasticenaZonaOznaka")
                        .HasColumnType("int");

                    b.HasKey("ZasticenaZonaID");

                    b.ToTable("ZasticeneZone");

                    b.HasData(
                        new
                        {
                            ZasticenaZonaID = new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"),
                            ZasticenaZonaOznaka = 1
                        },
                        new
                        {
                            ZasticenaZonaID = new Guid("9eec3d7d-2f21-4719-a8db-415806748dfb"),
                            ZasticenaZonaOznaka = 2
                        },
                        new
                        {
                            ZasticenaZonaID = new Guid("9d994da6-a766-4d67-971b-3b589b1ecbf8"),
                            ZasticenaZonaOznaka = 3
                        },
                        new
                        {
                            ZasticenaZonaID = new Guid("28ea362f-4e6c-4e0e-b853-b79c509a6b16"),
                            ZasticenaZonaOznaka = 4
                        });
                });

            modelBuilder.Entity("Parcela.Entities.DeoParceleEntity", b =>
                {
                    b.HasOne("Parcela.Entities.ParcelaEntity", "Parcela")
                        .WithMany("DeloviParcele")
                        .HasForeignKey("ParcelaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parcela");
                });

            modelBuilder.Entity("Parcela.Entities.ParcelaEntity", b =>
                {
                    b.HasOne("Parcela.Entities.KlasaEntity", "Klasa")
                        .WithMany("Parcele")
                        .HasForeignKey("KlasaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parcela.Entities.KulturaEntity", "Kultura")
                        .WithMany("Parcele")
                        .HasForeignKey("KulturaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parcela.Entities.OblikSvojineEntity", "OblikSvojine")
                        .WithMany("Parcele")
                        .HasForeignKey("OblikSvojineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parcela.Entities.ObradivostEntity", "Obradivost")
                        .WithMany("Parcele")
                        .HasForeignKey("ObradivostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parcela.Entities.OdvodnjavanjeEntity", "Odvodnjavanje")
                        .WithMany("Parcele")
                        .HasForeignKey("OdvodnjavanjeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Parcela.Entities.ZasticenaZonaEntity", "ZasticenaZona")
                        .WithMany("Parcele")
                        .HasForeignKey("ZasticenaZonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klasa");

                    b.Navigation("Kultura");

                    b.Navigation("OblikSvojine");

                    b.Navigation("Obradivost");

                    b.Navigation("Odvodnjavanje");

                    b.Navigation("ZasticenaZona");
                });

            modelBuilder.Entity("Parcela.Entities.KlasaEntity", b =>
                {
                    b.Navigation("Parcele");
                });

            modelBuilder.Entity("Parcela.Entities.KulturaEntity", b =>
                {
                    b.Navigation("Parcele");
                });

            modelBuilder.Entity("Parcela.Entities.OblikSvojineEntity", b =>
                {
                    b.Navigation("Parcele");
                });

            modelBuilder.Entity("Parcela.Entities.ObradivostEntity", b =>
                {
                    b.Navigation("Parcele");
                });

            modelBuilder.Entity("Parcela.Entities.OdvodnjavanjeEntity", b =>
                {
                    b.Navigation("Parcele");
                });

            modelBuilder.Entity("Parcela.Entities.ParcelaEntity", b =>
                {
                    b.Navigation("DeloviParcele");
                });

            modelBuilder.Entity("Parcela.Entities.ZasticenaZonaEntity", b =>
                {
                    b.Navigation("Parcele");
                });
#pragma warning restore 612, 618
        }
    }
}
