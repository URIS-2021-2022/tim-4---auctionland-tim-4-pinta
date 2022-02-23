using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parcela.Migrations
{
    public partial class InitialCreateParcela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klase",
                columns: table => new
                {
                    KlasaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlasaOznaka = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    { new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "I" },
                    { new Guid("18227841-6ba9-4509-b8fa-faa8f6699b3b"), "II" },
                    { new Guid("b417f2f5-5b3a-4856-a140-49a361d4cfd5"), "III" },
                    { new Guid("1d6f312f-73e5-4a57-9dd5-ba31f08bb967"), "IV" },
                    { new Guid("560475bd-3701-405b-9be8-e89768ce3eb5"), "V" },
                    { new Guid("d2c9a6ad-8c0d-44d2-8b56-2c4eada4ff99"), "VI" },
                    { new Guid("51c6ad49-5b78-424d-9cf5-259cb7d9e0e0"), "VII" },
                    { new Guid("9a011828-2b22-4666-b300-fe98e2c94d9a"), "VIII" }
                });

            migrationBuilder.InsertData(
                table: "Kulture",
                columns: new[] { "KulturaID", "KulturaNaziv" },
                values: new object[,]
                {
                    { new Guid("cb674d70-bd30-4ed5-bcc0-b5db489bfbe7"), "Sume" },
                    { new Guid("e7977a9e-74c7-4f4b-91b7-57fc03159456"), "Pasnjaci" },
                    { new Guid("67db5b31-6cb1-4bd0-a6a9-52702b06ced4"), "Livade" },
                    { new Guid("5f4a3a1e-3406-4991-abd4-0f095b59ac84"), "Trstici-mocvare" },
                    { new Guid("36840f4a-91c7-48b2-a85e-c2f285db0a56"), "Vocnjaci" },
                    { new Guid("86f5706f-737b-4b20-beed-531aa64326cb"), "Vrtovi" },
                    { new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Njive" },
                    { new Guid("04e29d95-7330-4d42-a10a-08556d478a46"), "Vinogradi" }
                });

            migrationBuilder.InsertData(
                table: "ObliciSvojine",
                columns: new[] { "OblikSvojineID", "OblikSvojineNaziv" },
                values: new object[,]
                {
                    { new Guid("085f566f-9900-45e4-800a-d679331b8050"), "Drugi oblici" },
                    { new Guid("64bda426-0a91-44fa-8da9-93cf24cc93ae"), "Zadruzna svojina" },
                    { new Guid("11474cc8-1ac3-47b5-87f0-c7de7f29f024"), "Drustvena svojina" },
                    { new Guid("2d83379c-87e6-45a1-9f00-321b820062fc"), "Mesovita svojina" },
                    { new Guid("91a1f792-bc28-4f6e-bdda-cb577d7858fe"), "Drzavna svojina RS" },
                    { new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), "Privatno" },
                    { new Guid("8cd557cd-b1ce-4a6e-8491-ddc80310d1e7"), "Drzavna svojina" }
                });

            migrationBuilder.InsertData(
                table: "Obradivosti",
                columns: new[] { "ObradivostID", "ObradivostNaziv" },
                values: new object[,]
                {
                    { new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivo" },
                    { new Guid("bf45ffef-1166-44fb-a2e1-67824a6561f2"), "Ostalo" }
                });

            migrationBuilder.InsertData(
                table: "Odvodnjavanja",
                columns: new[] { "OdvodnjavanjeID", "OdvodnjavanjeNaziv" },
                values: new object[,]
                {
                    { new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Povrsinsko" },
                    { new Guid("a2f44a7b-cdfb-4d69-b651-6d715afe8217"), "Podzemno" }
                });

            migrationBuilder.InsertData(
                table: "ZasticeneZone",
                columns: new[] { "ZasticenaZonaID", "ZasticenaZonaOznaka" },
                values: new object[,]
                {
                    { new Guid("9d994da6-a766-4d67-971b-3b589b1ecbf8"), 3 },
                    { new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), 1 },
                    { new Guid("9eec3d7d-2f21-4719-a8db-415806748dfb"), 2 },
                    { new Guid("28ea362f-4e6c-4e0e-b853-b79c509a6b16"), 4 }
                });

            migrationBuilder.InsertData(
                table: "Parcele",
                columns: new[] { "ParcelaID", "BrojListaNepokretnosti", "BrojParcele", "KatastarskaOpstinaID", "KlasaID", "KlasaStvarnoStanje", "KulturaID", "KulturaStvarnoStanje", "KupacID", "OblikSvojineID", "ObradivostID", "ObradivostStvarnoStanje", "OdvodnjavanjeID", "OdvodnjavanjeStvarnoStanje", "Povrsina", "ZasticenaZonaID", "ZasticenaZonaStvarnoStanje" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "12", "111", new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "I", new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Njive", new Guid("2a411c13-a195-48f7-8dbc-67596c3974c0"), new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivo", new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Povrsinsko", 10000, new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), "1" },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "92", "222", new Guid("177e64ad-2ff0-4a40-9c75-1f9b02ffe1e9"), new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "II", new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Vinogradi", new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivo", new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Podzemno", 2000, new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), "2" },
                    { new Guid("228927ab-e8fd-4d7e-8986-b9c3c4930480"), "54", "333", new Guid("177e64ad-2ff0-4a40-9c75-1f9b02ffe1e9"), new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "III", new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Vocnjaci", new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Ostalo", new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Podzemno", 3000, new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), "3" },
                    { new Guid("a913cb6f-9608-474a-88dc-4f38a51315ea"), "63", "444", new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), new Guid("829f5f3f-6159-4e15-ab52-d4c78ce944dc"), "I", new Guid("149b65ca-47aa-433c-8dbe-cdcf5e74a4ed"), "Njive", new Guid("2a411c13-a195-48f7-8dbc-67596c3974c0"), new Guid("0051339e-4bf1-4d63-89f9-d5f744016a2b"), new Guid("1fbc26e0-a797-45b8-bfb2-75d6799237ba"), "Obradivo", new Guid("32cf50d2-ab1a-45fb-a5de-f6c4fd646775"), "Povrsinsko", 4000, new Guid("a873025a-b4bc-440d-8e65-dc63fb9025d7"), "1" }
                });

            migrationBuilder.InsertData(
                table: "DeloviParcela",
                columns: new[] { "DeoParceleID", "ParcelaID", "PovrsinaDelaParcele", "RedniBroj" },
                values: new object[,]
                {
                    { new Guid("cae99a88-c6ee-4f4c-a463-419ac8fc1b85"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 2000, 1 },
                    { new Guid("c1df8ec6-dcae-4b27-bb33-7539fb6125c0"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 3000, 1 },
                    { new Guid("910876c8-ab40-4a29-a047-f9913ebaefb8"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 4000, 3 },
                    { new Guid("2884b2b0-302c-4eac-847c-65e4c356132b"), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), 2000, 1 }
                });

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
