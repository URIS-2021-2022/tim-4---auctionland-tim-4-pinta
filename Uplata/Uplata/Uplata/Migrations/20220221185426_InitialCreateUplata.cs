using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class InitialCreateUplata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uplata",
                columns: table => new
                {
                    UplataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Iznos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SvrhaUplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PozivNaBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Kurs_VrednostKursa = table.Column<double>(type: "float", nullable: true),
                    Kurs_Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Kurs_Valuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplata", x => x.UplataID);
                });

            migrationBuilder.InsertData(
                table: "Uplata",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"), "155-228523852256500-25", new DateTime(2022, 2, 21, 19, 54, 25, 671, DateTimeKind.Local).AddTicks(7368), "150000", null, "3121-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.InsertData(
                table: "Uplata",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"), "155-228523852256500-25", new DateTime(2022, 2, 21, 19, 54, 25, 686, DateTimeKind.Local).AddTicks(8867), "200000", null, "0242-424324523-444", "ucesce na licitaciji" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplata");
        }
    }
}
