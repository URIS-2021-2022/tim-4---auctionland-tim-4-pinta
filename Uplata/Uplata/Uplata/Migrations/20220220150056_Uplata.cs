using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class Uplata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Iznos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SvrhaUplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PozivNaBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataID);
                });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"), "155-228523852256500-25", new DateTime(2022, 2, 20, 16, 0, 56, 430, DateTimeKind.Local).AddTicks(7002), "150000", "3121-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"), "155-228523852256500-25", new DateTime(2022, 2, 20, 16, 0, 56, 432, DateTimeKind.Local).AddTicks(9445), "200000", "0242-424324523-444", "ucesce na licitaciji" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplate");
        }
    }
}
