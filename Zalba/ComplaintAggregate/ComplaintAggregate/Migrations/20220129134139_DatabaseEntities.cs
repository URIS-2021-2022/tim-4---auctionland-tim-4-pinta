using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplaintAggregate.Migrations
{
    public partial class DatabaseEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("5742a2d4-5ebc-466d-99ba-25d8d8dcd082"));

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("efbb80d6-e57b-4e45-9216-e23fc1de805b"));

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Radnja_na_osnovu_zalbe_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JN_ide_u_krug_sa_novim_uslovima = table.Column<bool>(type: "bit", nullable: false),
                    JN_ide_u_krug_sa_starim_uslovima = table.Column<bool>(type: "bit", nullable: false),
                    JN_ne_ide_u_drugi_krug = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Radnja_na_osnovu_zalbe_ID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Status_zalbe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usvojena = table.Column<bool>(type: "bit", nullable: false),
                    Odbijena = table.Column<bool>(type: "bit", nullable: false),
                    Otvorena = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Status_zalbe);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Tip_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Zalba_na_tok_javnog_nadmetanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zalba_na_odluku_o_davanju_na_zakup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zalba_na_odluku_o_davanju_na_koriscenje = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Tip_id);
                });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Radnja_na_osnovu_zalbe_ID", "JN_ide_u_krug_sa_novim_uslovima", "JN_ide_u_krug_sa_starim_uslovima", "JN_ne_ide_u_drugi_krug" },
                values: new object[,]
                {
                    { new Guid("07dea195-00ae-486a-8928-42921aec68bd"), false, true, false },
                    { new Guid("820965da-e3fe-4ef2-bed4-7b131101f771"), true, false, true }
                });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Razlog_zalbe" },
                values: new object[,]
                {
                    { new Guid("a9e48369-5661-41b7-bdf2-6059a0224554"), 32, 23, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje", "razlog" },
                    { new Guid("eff33630-25b3-4e9e-a3ee-61e21226bb9c"), 32, 23, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje2", "razlog2" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Status_zalbe", "Odbijena", "Otvorena", "Usvojena" },
                values: new object[,]
                {
                    { new Guid("5cf8c011-349e-46dd-a668-cdc180a089a4"), true, false, false },
                    { new Guid("698c3c91-33fc-4720-993f-f09bc1262814"), false, true, true }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Tip_id", "Zalba_na_odluku_o_davanju_na_koriscenje", "Zalba_na_odluku_o_davanju_na_zakup", "Zalba_na_tok_javnog_nadmetanja" },
                values: new object[,]
                {
                    { new Guid("27b9e209-ca15-4f71-b815-a060a6c96c2b"), "davanje na koriscenje", "davanje na zakup", "tok javnog nadmetanja" },
                    { new Guid("eebf3f9d-4457-41b4-a8aa-344f265f0109"), "davanje na koriscenje1", "davanje na zakup1", "tok javnog nadmetanja1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("a9e48369-5661-41b7-bdf2-6059a0224554"));

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("eff33630-25b3-4e9e-a3ee-61e21226bb9c"));

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Razlog_zalbe" },
                values: new object[] { new Guid("efbb80d6-e57b-4e45-9216-e23fc1de805b"), 32, 23, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje", "razlog" });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Razlog_zalbe" },
                values: new object[] { new Guid("5742a2d4-5ebc-466d-99ba-25d8d8dcd082"), 32, 23, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje2", "razlog2" });
        }
    }
}
