using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplaintAggregate.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ZalbaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datum_podnosenja_zalbe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Razlog_zalbe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obrazlozenje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum_rijesenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Broj_rijesenja = table.Column<int>(type: "int", nullable: false),
                    Broj_nadmetanja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ZalbaID);
                });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Razlog_zalbe" },
                values: new object[] { new Guid("efbb80d6-e57b-4e45-9216-e23fc1de805b"), 32, 23, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje", "razlog" });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Razlog_zalbe" },
                values: new object[] { new Guid("5742a2d4-5ebc-466d-99ba-25d8d8dcd082"), 32, 23, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje2", "razlog2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");
        }
    }
}
