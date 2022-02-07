using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KupacMikroservis.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fLica",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktOsoba = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DuzinaTrajanjaZabraneUGodinama = table.Column<int>(type: "int", nullable: false),
                    DatumPrestankaZabrane = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prioritet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OvlascenoLice = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fLica", x => x.KupacId);
                });

            migrationBuilder.CreateTable(
                name: "kOsobe",
                columns: table => new
                {
                    KontaktOsobaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funkcija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kOsobe", x => x.KontaktOsobaId);
                });

            migrationBuilder.CreateTable(
                name: "oLica",
                columns: table => new
                {
                    OvlascenoLiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojLicnogDokumenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTable = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oLica", x => x.OvlascenoLiceId);
                });

            migrationBuilder.CreateTable(
                name: "pLica",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaticniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DuzinaTrajanjaZabraneUGodinama = table.Column<int>(type: "int", nullable: false),
                    DatumPrestankaZabrane = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prioritet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OvlascenoLice = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pLica", x => x.KupacId);
                });

            migrationBuilder.CreateTable(
                name: "prioriteti",
                columns: table => new
                {
                    PrioritetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrioritetOpis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prioriteti", x => x.PrioritetId);
                });

            migrationBuilder.InsertData(
                table: "fLica",
                columns: new[] { "KupacId", "BrojRacuna", "BrojTelefona1", "BrojTelefona2", "DatumPocetkaZabrane", "DatumPrestankaZabrane", "DuzinaTrajanjaZabraneUGodinama", "Email", "ImaZabranu", "JMBG", "KontaktOsoba", "Naziv", "OvlascenoLice", "Prioritet" },
                values: new object[,]
                {
                    { new Guid("1a411c13-a195-48f7-8dbd-67596c3974c0"), "2532431234534", "062665511", "061553311", null, null, 0, "pera@gmail.com", false, "6765432484", new Guid("1a411c13-a195-3337-8dbd-44444c3974c0"), "Pera Peric", new Guid("1a411c13-a185-48f7-8dbd-67596c3974c8"), new Guid("1a411c13-a195-1117-8dbd-67596c3974c0") },
                    { new Guid("2a411c13-a195-48f7-8dbd-67596c3974c0"), "253425254534", "062665521", "061553331", null, null, 0, "jova@gmail.com", false, "7654321234", new Guid("1a411c13-a195-3337-8dbd-33333c3974c0"), "Jova Jovic", new Guid("1a411c13-a185-48f7-8dbd-67596c3975c8"), new Guid("1a411c13-a195-1117-8dbd-67596c3974c0") }
                });

            migrationBuilder.InsertData(
                table: "kOsobe",
                columns: new[] { "KontaktOsobaId", "Funkcija", "Ime", "Prezime", "Telefon" },
                values: new object[,]
                {
                    { new Guid("1a411c13-a195-3337-8dbd-33333c3974c0"), "fja1", "Ana", "Ankovic", "65432351" },
                    { new Guid("1a411c13-a195-3337-8dbd-44444c3974c0"), "fja2", "Milos", "Milosevic", "5432114" }
                });

            migrationBuilder.InsertData(
                table: "oLica",
                columns: new[] { "OvlascenoLiceId", "BrojLicnogDokumenta", "BrojTable", "Ime", "Prezime" },
                values: new object[,]
                {
                    { new Guid("1a411c13-a195-3337-8dbd-11111c3974c0"), "565423433", "54356543", "Petar", "Petrosevic" },
                    { new Guid("1a411c13-a195-3337-8dbd-22222c3974c0"), "5653424", "543231313", "Luka", "Lukovic" }
                });

            migrationBuilder.InsertData(
                table: "pLica",
                columns: new[] { "KupacId", "BrojRacuna", "BrojTelefona1", "BrojTelefona2", "DatumPocetkaZabrane", "DatumPrestankaZabrane", "DuzinaTrajanjaZabraneUGodinama", "Email", "Faks", "ImaZabranu", "MaticniBroj", "Naziv", "OvlascenoLice", "Prioritet" },
                values: new object[,]
                {
                    { new Guid("2a411c13-a195-48f7-8dbc-67596c3974c0"), "2536565534", "062665231", "0615573331", null, null, 0, "ivaa@gmail.com", "654322345", false, "455643231", "NS DOO", new Guid("1a411c13-a185-48f7-8dbd-67596c3975c8"), new Guid("1a411c13-a195-1117-8dbd-67596c3974c0") },
                    { new Guid("2a421c13-a195-46f7-8dbd-67596c4974c0"), "253456533534", "062635321", "0615535651", null, null, 0, "mikaa@gmail.com", "654322345", false, "455643231", "SN AD", new Guid("1a411c13-a185-48f7-8dbd-67596c3975c8"), new Guid("1a411c13-a195-1117-8dbd-67596c3974c0") }
                });

            migrationBuilder.InsertData(
                table: "prioriteti",
                columns: new[] { "PrioritetId", "PrioritetOpis" },
                values: new object[,]
                {
                    { new Guid("1a411c13-a195-1117-8dbd-67596c3974c0"), "Visok" },
                    { new Guid("1a411c13-a195-2227-8dbd-67596c3974c0"), "Srednji" },
                    { new Guid("1a411c13-a195-3337-8dbd-67596c3974c0"), "Nizak" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fLica");

            migrationBuilder.DropTable(
                name: "kOsobe");

            migrationBuilder.DropTable(
                name: "oLica");

            migrationBuilder.DropTable(
                name: "pLica");

            migrationBuilder.DropTable(
                name: "prioriteti");
        }
    }
}
