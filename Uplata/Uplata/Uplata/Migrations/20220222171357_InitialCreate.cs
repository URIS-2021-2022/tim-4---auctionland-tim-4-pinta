using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Uplata",
                table: "Uplata");

            migrationBuilder.DropColumn(
                name: "Kurs_Datum",
                table: "Uplata");

            migrationBuilder.DropColumn(
                name: "Kurs_Valuta",
                table: "Uplata");

            migrationBuilder.DropColumn(
                name: "Kurs_VrednostKursa",
                table: "Uplata");

            migrationBuilder.RenameTable(
                name: "Uplata",
                newName: "Uplate");

            migrationBuilder.AddColumn<Guid>(
                name: "KursID",
                table: "Uplate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uplate",
                table: "Uplate",
                column: "UplataID");

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

            migrationBuilder.InsertData(
                table: "Kursevi",
                columns: new[] { "KursID", "Datum", "Valuta", "VrednostKursa" },
                values: new object[] { new Guid("b06a4284-44e2-46af-8d74-b79c8b0c6017"), new DateTime(2022, 2, 22, 18, 13, 56, 661, DateTimeKind.Local).AddTicks(9715), "EUR", 117.8 });

            migrationBuilder.InsertData(
                table: "Kursevi",
                columns: new[] { "KursID", "Datum", "Valuta", "VrednostKursa" },
                values: new object[] { new Guid("411c4082-cc5e-4f5f-8946-4086ebca08d0"), new DateTime(2022, 2, 22, 18, 13, 56, 666, DateTimeKind.Local).AddTicks(5875), "GBT", 150.5 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                columns: new[] { "Datum", "Iznos", "KursID" },
                values: new object[] { new DateTime(2022, 2, 22, 18, 13, 56, 666, DateTimeKind.Local).AddTicks(7318), "100", new Guid("b06a4284-44e2-46af-8d74-b79c8b0c6017") });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                columns: new[] { "Datum", "Iznos", "KursID" },
                values: new object[] { new DateTime(2022, 2, 22, 18, 13, 56, 666, DateTimeKind.Local).AddTicks(6132), "200", new Guid("411c4082-cc5e-4f5f-8946-4086ebca08d0") });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "UplataID", "BrojRacuna", "Datum", "Iznos", "JavnoNadmetanjeID", "KursID", "PozivNaBroj", "SvrhaUplate" },
                values: new object[] { new Guid("1d2ed242-5059-4a1b-aeab-eee99404284f"), "115-228523852256500-25", new DateTime(2022, 2, 22, 18, 13, 56, 666, DateTimeKind.Local).AddTicks(7363), "50", null, new Guid("411c4082-cc5e-4f5f-8946-4086ebca08d0"), "3221-424324523-444", "ucesce na licitaciji" });

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KursID",
                table: "Uplate",
                column: "KursID");

            migrationBuilder.AddForeignKey(
                name: "FK_Uplate_Kursevi_KursID",
                table: "Uplate",
                column: "KursID",
                principalTable: "Kursevi",
                principalColumn: "KursID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uplate_Kursevi_KursID",
                table: "Uplate");

            migrationBuilder.DropTable(
                name: "Kursevi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Uplate",
                table: "Uplate");

            migrationBuilder.DropIndex(
                name: "IX_Uplate_KursID",
                table: "Uplate");

            migrationBuilder.DeleteData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("1d2ed242-5059-4a1b-aeab-eee99404284f"));

            migrationBuilder.DropColumn(
                name: "KursID",
                table: "Uplate");

            migrationBuilder.RenameTable(
                name: "Uplate",
                newName: "Uplata");

            migrationBuilder.AddColumn<DateTime>(
                name: "Kurs_Datum",
                table: "Uplata",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kurs_Valuta",
                table: "Uplata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Kurs_VrednostKursa",
                table: "Uplata",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uplata",
                table: "Uplata",
                column: "UplataID");

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                columns: new[] { "Datum", "Iznos" },
                values: new object[] { new DateTime(2022, 2, 21, 19, 54, 25, 686, DateTimeKind.Local).AddTicks(8867), "200000" });

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                columns: new[] { "Datum", "Iznos" },
                values: new object[] { new DateTime(2022, 2, 21, 19, 54, 25, 671, DateTimeKind.Local).AddTicks(7368), "150000" });
        }
    }
}
