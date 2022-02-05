using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavnoNadmetanjeAgregat.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SluzbeniListovi",
                columns: table => new
                {
                    SluzbeniListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Opstina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojSluzbenogLista = table.Column<int>(type: "int", nullable: false),
                    DatumIzdavanjaSluzbenogLista = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SluzbeniListovi", x => x.SluzbeniListID);
                });

            migrationBuilder.CreateTable(
                name: "StatusiJavnihNadmetanja",
                columns: table => new
                {
                    StatusJavnogNadmetanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivStatusaJavnogNadmetanja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusiJavnihNadmetanja", x => x.StatusJavnogNadmetanjaID);
                });

            migrationBuilder.CreateTable(
                name: "TipoviJavnihNadmetanja",
                columns: table => new
                {
                    TipJavnogNadmetanjaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivTipaJavnogNadmetanja = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviJavnihNadmetanja", x => x.TipJavnogNadmetanjaID);
                });

            migrationBuilder.CreateTable(
                name: "JavnaNadmetanja",
                columns: table => new
                {
                    JavnoNadmetanjeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremePocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeKraja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PocetnaCenaPoHektaru = table.Column<int>(type: "int", nullable: false),
                    PeriodZakupa = table.Column<int>(type: "int", nullable: false),
                    Izuzeto = table.Column<bool>(type: "bit", nullable: false),
                    TipID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Krug = table.Column<int>(type: "int", nullable: false),
                    VisinaDopuneDepozita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavnaNadmetanja", x => x.JavnoNadmetanjeID);
                    table.ForeignKey(
                        name: "FK_JavnaNadmetanja_StatusiJavnihNadmetanja_StatusID",
                        column: x => x.StatusID,
                        principalTable: "StatusiJavnihNadmetanja",
                        principalColumn: "StatusJavnogNadmetanjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JavnaNadmetanja_TipoviJavnihNadmetanja_TipID",
                        column: x => x.TipID,
                        principalTable: "TipoviJavnihNadmetanja",
                        principalColumn: "TipJavnogNadmetanjaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SluzbeniListovi",
                columns: new[] { "SluzbeniListID", "BrojSluzbenogLista", "DatumIzdavanjaSluzbenogLista", "Opstina" },
                values: new object[,]
                {
                    { new Guid("102e134d-ffde-40fa-b355-f0b8bc52f886"), 12, new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beograd" },
                    { new Guid("901b0ad2-6aa8-4076-8162-01b3a42f2a2e"), 13, new DateTime(2021, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Novi Sad" }
                });

            migrationBuilder.InsertData(
                table: "StatusiJavnihNadmetanja",
                columns: new[] { "StatusJavnogNadmetanjaID", "NazivStatusaJavnogNadmetanja" },
                values: new object[,]
                {
                    { new Guid("bf50e668-c01a-46e3-bae8-a1691c23c65f"), "Status1" },
                    { new Guid("b38e3b4f-5539-4475-8424-00ca7a59e496"), "Status2" }
                });

            migrationBuilder.InsertData(
                table: "TipoviJavnihNadmetanja",
                columns: new[] { "TipJavnogNadmetanjaID", "NazivTipaJavnogNadmetanja" },
                values: new object[,]
                {
                    { new Guid("4d51c54c-4b90-46de-8bb2-c8f74fb6fd9e"), "Tip1" },
                    { new Guid("0f173f98-c00a-4eb4-8131-ae00177371d8"), "Tip2" }
                });

            migrationBuilder.InsertData(
                table: "JavnaNadmetanja",
                columns: new[] { "JavnoNadmetanjeID", "Datum", "Izuzeto", "Krug", "PeriodZakupa", "PocetnaCenaPoHektaru", "StatusID", "TipID", "VisinaDopuneDepozita", "VremeKraja", "VremePocetka" },
                values: new object[] { new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 2, 1000, new Guid("bf50e668-c01a-46e3-bae8-a1691c23c65f"), new Guid("4d51c54c-4b90-46de-8bb2-c8f74fb6fd9e"), 10, new DateTime(2021, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "JavnaNadmetanja",
                columns: new[] { "JavnoNadmetanjeID", "Datum", "Izuzeto", "Krug", "PeriodZakupa", "PocetnaCenaPoHektaru", "StatusID", "TipID", "VisinaDopuneDepozita", "VremeKraja", "VremePocetka" },
                values: new object[] { new Guid("7c7764e0-27a2-4123-9eb4-081c4e9bcdbf"), new DateTime(2021, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 2, 1000, new Guid("bf50e668-c01a-46e3-bae8-a1691c23c65f"), new Guid("4d51c54c-4b90-46de-8bb2-c8f74fb6fd9e"), 10, new DateTime(2021, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_JavnaNadmetanja_StatusID",
                table: "JavnaNadmetanja",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_JavnaNadmetanja_TipID",
                table: "JavnaNadmetanja",
                column: "TipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JavnaNadmetanja");

            migrationBuilder.DropTable(
                name: "SluzbeniListovi");

            migrationBuilder.DropTable(
                name: "StatusiJavnihNadmetanja");

            migrationBuilder.DropTable(
                name: "TipoviJavnihNadmetanja");
        }
    }
}
