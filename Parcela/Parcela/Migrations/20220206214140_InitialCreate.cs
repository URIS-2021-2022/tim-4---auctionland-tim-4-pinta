using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parcela.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klase",
                columns: table => new
                {
                    KlasaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlasaOznaka = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klase", x => x.KlasaID);
                });

            migrationBuilder.CreateTable(
                name: "Kulture",
                columns: table => new
                {
                    KulturaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KulturaNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kulture", x => x.KulturaID);
                });

            migrationBuilder.CreateTable(
                name: "ObliciSvojine",
                columns: table => new
                {
                    OblikSvojineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OblikSvojineNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObliciSvojine", x => x.OblikSvojineID);
                });

            migrationBuilder.CreateTable(
                name: "Obradivosti",
                columns: table => new
                {
                    ObradivostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObradivostNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obradivosti", x => x.ObradivostID);
                });

            migrationBuilder.CreateTable(
                name: "Odvodnjavanja",
                columns: table => new
                {
                    OdvodnjavanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OdvodnjavanjeNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odvodnjavanja", x => x.OdvodnjavanjeID);
                });

            migrationBuilder.CreateTable(
                name: "ZasticeneZone",
                columns: table => new
                {
                    ZasticenaZonaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZasticenaZonaOznaka = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZasticeneZone", x => x.ZasticenaZonaID);
                });

            migrationBuilder.CreateTable(
                name: "Parcele",
                columns: table => new
                {
                    ParcelaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Povrsina = table.Column<int>(type: "int", nullable: false),
                    BrojParcele = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojListaNepokretnosti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KulturaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KlasaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObradivostStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZasticenaZonaStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdvodnjavanjeStvarnoStanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZasticenaZonaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OdvodnjavanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObradivostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OblikSvojineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KulturaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlasaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KatastarskaOpstinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcele", x => x.ParcelaID);
                    table.ForeignKey(
                        name: "FK_Parcele_Klase_KlasaID",
                        column: x => x.KlasaID,
                        principalTable: "Klase",
                        principalColumn: "KlasaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcele_Kulture_KulturaID",
                        column: x => x.KulturaID,
                        principalTable: "Kulture",
                        principalColumn: "KulturaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcele_ObliciSvojine_OblikSvojineID",
                        column: x => x.OblikSvojineID,
                        principalTable: "ObliciSvojine",
                        principalColumn: "OblikSvojineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcele_Obradivosti_ObradivostID",
                        column: x => x.ObradivostID,
                        principalTable: "Obradivosti",
                        principalColumn: "ObradivostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcele_Odvodnjavanja_OdvodnjavanjeID",
                        column: x => x.OdvodnjavanjeID,
                        principalTable: "Odvodnjavanja",
                        principalColumn: "OdvodnjavanjeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcele_ZasticeneZone_ZasticenaZonaID",
                        column: x => x.ZasticenaZonaID,
                        principalTable: "ZasticeneZone",
                        principalColumn: "ZasticenaZonaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeloviParcela",
                columns: table => new
                {
                    DeoParceleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RedniBroj = table.Column<int>(type: "int", nullable: false),
                    PovrsinaDelaParcele = table.Column<int>(type: "int", nullable: false),
                    ParcelaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeloviParcela", x => x.DeoParceleID);
                    table.ForeignKey(
                        name: "FK_DeloviParcela_Parcele_ParcelaID",
                        column: x => x.ParcelaID,
                        principalTable: "Parcele",
                        principalColumn: "ParcelaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Klase",
                columns: new[] { "KlasaID", "KlasaOznaka" },
                values: new object[,]
                {
                    { new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), 1 },
                    { new Guid("18227841-6ba9-4509-b8fa-faa8f6699b3b"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Kulture",
                columns: new[] { "KulturaID", "KulturaNaziv" },
                values: new object[,]
                {
                    { new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Kukuruz" },
                    { new Guid("86f5706f-737b-4b20-beed-531aa64326cb"), "Soja" }
                });

            migrationBuilder.InsertData(
                table: "ObliciSvojine",
                columns: new[] { "OblikSvojineID", "OblikSvojineNaziv" },
                values: new object[,]
                {
                    { new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), "Oblik svojine 1" },
                    { new Guid("91a1f792-bc28-4f6e-bdda-cb577d7858fe"), "Oblik svojine 2" }
                });

            migrationBuilder.InsertData(
                table: "Obradivosti",
                columns: new[] { "ObradivostID", "ObradivostNaziv" },
                values: new object[,]
                {
                    { new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivost1" },
                    { new Guid("bf45ffef-1166-44fb-a2e1-67824a6561f2"), "Obradivost2" }
                });

            migrationBuilder.InsertData(
                table: "Odvodnjavanja",
                columns: new[] { "OdvodnjavanjeID", "OdvodnjavanjeNaziv" },
                values: new object[,]
                {
                    { new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Odvodnjavanje1" },
                    { new Guid("a2f44a7b-cdfb-4d69-b651-6d715afe8217"), "Odvodnjavanje2" }
                });

            migrationBuilder.InsertData(
                table: "ZasticeneZone",
                columns: new[] { "ZasticenaZonaID", "ZasticenaZonaOznaka" },
                values: new object[,]
                {
                    { new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), 1 },
                    { new Guid("9eec3d7d-2f21-4719-a8db-415806748dfb"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Parcele",
                columns: new[] { "ParcelaID", "BrojListaNepokretnosti", "BrojParcele", "KatastarskaOpstinaID", "KlasaID", "KlasaStvarnoStanje", "KulturaID", "KulturaStvarnoStanje", "KupacID", "OblikSvojineID", "ObradivostID", "ObradivostStvarnoStanje", "OdvodnjavanjeID", "OdvodnjavanjeStvarnoStanje", "Povrsina", "ZasticenaZonaID", "ZasticenaZonaStvarnoStanje" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "12345", "12345", new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "Klasa1", new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Kukuruz", new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivost1", new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Odvodnjavanje1", 1000, new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), "ZasticenaZona1" });

            migrationBuilder.InsertData(
                table: "Parcele",
                columns: new[] { "ParcelaID", "BrojListaNepokretnosti", "BrojParcele", "KatastarskaOpstinaID", "KlasaID", "KlasaStvarnoStanje", "KulturaID", "KulturaStvarnoStanje", "KupacID", "OblikSvojineID", "ObradivostID", "ObradivostStvarnoStanje", "OdvodnjavanjeID", "OdvodnjavanjeStvarnoStanje", "Povrsina", "ZasticenaZonaID", "ZasticenaZonaStvarnoStanje" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "54321", "54321", new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "Klasa2", new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Soja", new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivost2", new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Odvodnjavanje2", 2000, new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), "ZasticenaZona2" });

            migrationBuilder.InsertData(
                table: "DeloviParcela",
                columns: new[] { "DeoParceleID", "ParcelaID", "PovrsinaDelaParcele", "RedniBroj" },
                values: new object[] { new Guid("cae99a88-c6ee-4f4c-a463-419ac8fc1b85"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 1000, 1 });

            migrationBuilder.InsertData(
                table: "DeloviParcela",
                columns: new[] { "DeoParceleID", "ParcelaID", "PovrsinaDelaParcele", "RedniBroj" },
                values: new object[] { new Guid("2884b2b0-302c-4eac-847c-65e4c356132b"), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), 2000, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_DeloviParcela_ParcelaID",
                table: "DeloviParcela",
                column: "ParcelaID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_KlasaID",
                table: "Parcele",
                column: "KlasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_KulturaID",
                table: "Parcele",
                column: "KulturaID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_OblikSvojineID",
                table: "Parcele",
                column: "OblikSvojineID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_ObradivostID",
                table: "Parcele",
                column: "ObradivostID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_OdvodnjavanjeID",
                table: "Parcele",
                column: "OdvodnjavanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Parcele_ZasticenaZonaID",
                table: "Parcele",
                column: "ZasticenaZonaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeloviParcela");

            migrationBuilder.DropTable(
                name: "Parcele");

            migrationBuilder.DropTable(
                name: "Klase");

            migrationBuilder.DropTable(
                name: "Kulture");

            migrationBuilder.DropTable(
                name: "ObliciSvojine");

            migrationBuilder.DropTable(
                name: "Obradivosti");

            migrationBuilder.DropTable(
                name: "Odvodnjavanja");

            migrationBuilder.DropTable(
                name: "ZasticeneZone");
        }
    }
}
