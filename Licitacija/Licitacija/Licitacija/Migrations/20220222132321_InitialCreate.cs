using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licitacija.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licitacije",
                columns: table => new
                {
                    LicitacijaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ogranicenje = table.Column<int>(type: "int", nullable: false),
                    KorakCene = table.Column<int>(type: "int", nullable: false),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DokFizickog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DokPravnog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rok = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacije", x => x.LicitacijaID);
                });

            migrationBuilder.InsertData(
                table: "Licitacije",
                columns: new[] { "LicitacijaID", "Broj", "Datum", "DokFizickog", "DokPravnog", "Godina", "JavnoNadmetanjeID", "KorakCene", "KupacID", "Ogranicenje", "Rok" },
                values: new object[] { new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"), 1, new DateTime(2022, 2, 22, 14, 23, 20, 874, DateTimeKind.Local).AddTicks(5198), "Dokument1", "Dokument1", 2002, new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"), 100, new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"), 100, new DateTime(2022, 2, 22, 14, 23, 20, 875, DateTimeKind.Local).AddTicks(9164) });

            migrationBuilder.InsertData(
                table: "Licitacije",
                columns: new[] { "LicitacijaID", "Broj", "Datum", "DokFizickog", "DokPravnog", "Godina", "JavnoNadmetanjeID", "KorakCene", "KupacID", "Ogranicenje", "Rok" },
                values: new object[] { new Guid("4879ec40-df11-457c-9bd1-07cd2b4ec7cd"), 2, new DateTime(2022, 2, 22, 14, 23, 20, 876, DateTimeKind.Local).AddTicks(7922), "Dokument2", "Dokument2", 2020, new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"), 200, new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"), 200, new DateTime(2022, 2, 22, 14, 23, 20, 876, DateTimeKind.Local).AddTicks(7936) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitacije");
        }
    }
}
