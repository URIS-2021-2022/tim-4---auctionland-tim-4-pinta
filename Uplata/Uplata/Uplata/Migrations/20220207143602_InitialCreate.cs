using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class InitialCreate : Migration
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
                    KupacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataID);
                });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "Datum", "Iznos", "JavnoNadmetanjeID", "KupacID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(2022, 2, 7, 15, 36, 2, 89, DateTimeKind.Local).AddTicks(6851), "150000", new Guid("6a411c23-a192-48f7-8dbd-67596c3974c0"), new Guid("6a411c23-a195-48f7-8dbd-67596c3974c0"), "3121-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "Datum", "Iznos", "JavnoNadmetanjeID", "KupacID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("7a411c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(2022, 2, 7, 15, 36, 2, 91, DateTimeKind.Local).AddTicks(9895), "200000", new Guid("6a411c23-a192-48f7-8dbd-67596c3974c0"), new Guid("6a411c23-a195-48f7-8dbd-67596c3974c0"), "0242-424324523-444", "ucesce na licitaciji" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplate");
        }
    }
}
