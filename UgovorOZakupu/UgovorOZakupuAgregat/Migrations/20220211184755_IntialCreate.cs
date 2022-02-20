using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UgovorOZakupuAgregat.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dokumenti",
                columns: table => new
                {
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZavodniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDonosenjaDokumenta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenti", x => x.DokumentId);
                });

            migrationBuilder.CreateTable(
                name: "TipoviGarancije",
                columns: table => new
                {
                    TipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviGarancije", x => x.TipId);
                });

            migrationBuilder.CreateTable(
                name: "Ugovori",
                columns: table => new
                {
                    UgovorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZavodniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumZavodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RokZaVracanjeZemljista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoPotpisivanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumPotpisa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugovori", x => x.UgovorId);
                    table.ForeignKey(
                        name: "FK_Ugovori_Dokumenti_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokumenti",
                        principalColumn: "DokumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ugovori_TipoviGarancije_TipId",
                        column: x => x.TipId,
                        principalTable: "TipoviGarancije",
                        principalColumn: "TipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RokoviDospeca",
                columns: table => new
                {
                    RokId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UgovorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RokDospeca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RokoviDospeca", x => x.RokId);
                    table.ForeignKey(
                        name: "FK_RokoviDospeca_Ugovori_UgovorId",
                        column: x => x.UgovorId,
                        principalTable: "Ugovori",
                        principalColumn: "UgovorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dokumenti",
                columns: new[] { "DokumentId", "Datum", "DatumDonosenjaDokumenta", "ZavodniBroj" },
                values: new object[,]
                {
                    { new Guid("d1209104-7358-4c22-9f4f-415203563a25"), new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234a" },
                    { new Guid("b030fb0d-7f19-4341-9723-cddb3ddd6980"), new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "123a" }
                });

            migrationBuilder.InsertData(
                table: "TipoviGarancije",
                columns: new[] { "TipId", "Naziv" },
                values: new object[,]
                {
                    { new Guid("e1f134e5-25f9-4b00-8b96-a809d11cd33b"), "Jemstvo" },
                    { new Guid("234d1ada-07b8-4789-9c87-86b83118fed0"), "Bankarska Garancija" }
                });

            migrationBuilder.InsertData(
                table: "Ugovori",
                columns: new[] { "UgovorId", "DatumPotpisa", "DatumZavodjenja", "DokumentId", "MestoPotpisivanja", "RokZaVracanjeZemljista", "TipId", "ZavodniBroj" },
                values: new object[] { new Guid("407c6e21-0765-44e9-a34b-b2c387814e55"), new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d1209104-7358-4c22-9f4f-415203563a25"), "Novi Sad", new DateTime(2021, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1f134e5-25f9-4b00-8b96-a809d11cd33b"), "11a" });

            migrationBuilder.InsertData(
                table: "RokoviDospeca",
                columns: new[] { "RokId", "RokDospeca", "UgovorId" },
                values: new object[] { new Guid("234d1ada-07b8-4789-9c87-86b83118fed0"), 1, new Guid("407c6e21-0765-44e9-a34b-b2c387814e55") });

            migrationBuilder.InsertData(
                table: "RokoviDospeca",
                columns: new[] { "RokId", "RokDospeca", "UgovorId" },
                values: new object[] { new Guid("5bed6b27-02a9-484b-b3c7-768b604ff77e"), 2, new Guid("407c6e21-0765-44e9-a34b-b2c387814e55") });

            migrationBuilder.CreateIndex(
                name: "IX_RokoviDospeca_UgovorId",
                table: "RokoviDospeca",
                column: "UgovorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_DokumentId",
                table: "Ugovori",
                column: "DokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ugovori_TipId",
                table: "Ugovori",
                column: "TipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RokoviDospeca");

            migrationBuilder.DropTable(
                name: "Ugovori");

            migrationBuilder.DropTable(
                name: "Dokumenti");

            migrationBuilder.DropTable(
                name: "TipoviGarancije");
        }
    }
}
