using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class IzmenaKursVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 17, 58, 55, 704, DateTimeKind.Local).AddTicks(5879));

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 17, 58, 55, 702, DateTimeKind.Local).AddTicks(3850));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kurs_Datum",
                table: "Uplata");

            migrationBuilder.DropColumn(
                name: "Kurs_Valuta",
                table: "Uplata");

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 17, 52, 12, 593, DateTimeKind.Local).AddTicks(5796));

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 17, 52, 12, 591, DateTimeKind.Local).AddTicks(174));
        }
    }
}
