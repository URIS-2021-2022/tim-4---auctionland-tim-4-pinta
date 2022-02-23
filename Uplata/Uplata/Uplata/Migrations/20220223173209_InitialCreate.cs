using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kursevi",
                columns: table => new
                {
                    KursID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VrednostKursa = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursevi", x => x.KursID);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Iznos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SvrhaUplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PozivNaBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KursID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataID);
                    table.ForeignKey(
                        name: "FK_Uplate_Kursevi_KursID",
                        column: x => x.KursID,
                        principalTable: "Kursevi",
                        principalColumn: "KursID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kursevi",
                columns: new[] { "KursID", "Datum", "Valuta", "VrednostKursa" },
                values: new object[] { new Guid("b06a4284-44e2-46af-8d74-b79c8b0c6017"), new DateTime(2022, 2, 23, 18, 32, 9, 474, DateTimeKind.Local).AddTicks(3238), "EUR", 117.8 });

            migrationBuilder.InsertData(
                table: "Kursevi",
                columns: new[] { "KursID", "Datum", "Valuta", "VrednostKursa" },
                values: new object[] { new Guid("411c4082-cc5e-4f5f-8946-4086ebca08d0"), new DateTime(2022, 2, 23, 18, 32, 9, 476, DateTimeKind.Local).AddTicks(6065), "GBT", 150.5 });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "KursID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"), "155-228523852256500-25", new DateTime(2022, 2, 23, 18, 32, 9, 476, DateTimeKind.Local).AddTicks(7083), "100", new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), new Guid("b06a4284-44e2-46af-8d74-b79c8b0c6017"), "0242-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "KursID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"), "155-228523852256500-25", new DateTime(2022, 2, 23, 18, 32, 9, 476, DateTimeKind.Local).AddTicks(6204), "200", new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), new Guid("411c4082-cc5e-4f5f-8946-4086ebca08d0"), "3121-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "KursID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("1d2ed242-5059-4a1b-aeab-eee99404284f"), "115-228523852256500-25", new DateTime(2022, 2, 23, 18, 32, 9, 476, DateTimeKind.Local).AddTicks(7113), "50", new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), new Guid("411c4082-cc5e-4f5f-8946-4086ebca08d0"), "3221-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KursID",
                table: "Uplate",
                column: "KursID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "Kursevi");
        }
    }
}
