using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class DodavanjeKursVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Uplate",
                table: "Uplate");

            migrationBuilder.RenameTable(
                name: "Uplate",
                newName: "Uplata");

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
                column: "Datum",
                value: new DateTime(2022, 2, 20, 17, 52, 12, 593, DateTimeKind.Local).AddTicks(5796));

            migrationBuilder.UpdateData(
                table: "Uplata",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 17, 52, 12, 591, DateTimeKind.Local).AddTicks(174));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Uplata",
                table: "Uplata");

            migrationBuilder.DropColumn(
                name: "Kurs_VrednostKursa",
                table: "Uplata");

            migrationBuilder.RenameTable(
                name: "Uplata",
                newName: "Uplate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uplate",
                table: "Uplate",
                column: "UplataID");

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 16, 10, 3, 831, DateTimeKind.Local).AddTicks(4387));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 16, 10, 3, 829, DateTimeKind.Local).AddTicks(1181));
        }
    }
}
