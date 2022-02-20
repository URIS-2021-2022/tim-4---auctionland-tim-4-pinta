using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licnost.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licnosti",
                columns: table => new
                {
                    LicnostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicnostIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicnostPrezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicnostFunkcija = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licnosti", x => x.LicnostId);
                });

            migrationBuilder.CreateTable(
                name: "Komisije",
                columns: table => new
                {
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicnostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komisije", x => x.KomisijaId);
                    table.ForeignKey(
                        name: "FK_Komisije_Licnosti_LicnostId",
                        column: x => x.LicnostId,
                        principalTable: "Licnosti",
                        principalColumn: "LicnostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClanoviKomisije",
                columns: table => new
                {
                    ClanKomisijeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicnostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KomisijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanoviKomisije", x => x.ClanKomisijeId);
                    table.ForeignKey(
                        name: "FK_ClanoviKomisije_Komisije_KomisijaId",
                        column: x => x.KomisijaId,
                        principalTable: "Komisije",
                        principalColumn: "KomisijaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ClanoviKomisije_Licnosti_LicnostId",
                        column: x => x.LicnostId,
                        principalTable: "Licnosti",
                        principalColumn: "LicnostId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Licnosti",
                columns: new[] { "LicnostId", "LicnostFunkcija", "LicnostIme", "LicnostPrezime" },
                values: new object[] { new Guid("e91b29cc-79a5-4de8-8030-77df6e514def"), "IT", "Simona", "Bolehradsky" });

            migrationBuilder.InsertData(
                table: "Licnosti",
                columns: new[] { "LicnostId", "LicnostFunkcija", "LicnostIme", "LicnostPrezime" },
                values: new object[] { new Guid("218c05c6-5066-4354-9568-b263ab11713b"), "Direktor", "Dajana", "Jelic" });

            migrationBuilder.InsertData(
                table: "Komisije",
                columns: new[] { "KomisijaId", "LicnostId" },
                values: new object[] { new Guid("3540bf55-427a-4892-91d6-633d683ef0ed"), new Guid("e91b29cc-79a5-4de8-8030-77df6e514def") });

            migrationBuilder.InsertData(
                table: "ClanoviKomisije",
                columns: new[] { "ClanKomisijeId", "KomisijaId", "LicnostId" },
                values: new object[] { new Guid("049c45e5-8873-49c2-8275-0b63293f15e7"), new Guid("3540bf55-427a-4892-91d6-633d683ef0ed"), new Guid("218c05c6-5066-4354-9568-b263ab11713b") });

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviKomisije_KomisijaId",
                table: "ClanoviKomisije",
                column: "KomisijaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviKomisije_LicnostId",
                table: "ClanoviKomisije",
                column: "LicnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Komisije_LicnostId",
                table: "Komisije",
                column: "LicnostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanoviKomisije");

            migrationBuilder.DropTable(
                name: "Komisije");

            migrationBuilder.DropTable(
                name: "Licnosti");
        }
    }
}
