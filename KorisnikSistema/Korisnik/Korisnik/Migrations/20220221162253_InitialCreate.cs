using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Korisnik.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KorisnikModels",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikModels", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    tokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnikId = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.tokenId);
                });

            migrationBuilder.InsertData(
                table: "KorisnikModels",
                columns: new[] { "KorisnikId", "Ime", "KorisnickoIme", "Lozinka", "Prezime", "Salt", "TipKorisnika" },
                values: new object[] { 2, "Petar", "IT1/2020", "1", "Petrović", "1", "administrator" });

            migrationBuilder.InsertData(
                table: "KorisnikModels",
                columns: new[] { "KorisnikId", "Ime", "KorisnickoIme", "Lozinka", "Prezime", "Salt", "TipKorisnika" },
                values: new object[] { 3, "Marko", "IT2/2019", "1", "Marković", "1", "licitant" });

            migrationBuilder.InsertData(
                table: "Tokens",
                columns: new[] { "tokenId", "korisnikId", "time", "token" },
                values: new object[] { 1, 3, new DateTime(2000, 10, 10, 10, 10, 10, 0, DateTimeKind.Unspecified), "token" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikModels");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
