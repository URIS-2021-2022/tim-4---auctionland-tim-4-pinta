using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class JavnoNadmetanjeKolona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JavnoNadmetanjeID",
                table: "Uplate",
                type: "uniqueidentifier",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JavnoNadmetanjeID",
                table: "Uplate");

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("5f951cf9-aaf2-45c3-823a-5c8c4c1deaff"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 16, 0, 56, 432, DateTimeKind.Local).AddTicks(9445));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("8d452221-f73e-4e35-ba7c-3fdd0d08be70"),
                column: "Datum",
                value: new DateTime(2022, 2, 20, 16, 0, 56, 430, DateTimeKind.Local).AddTicks(7002));
        }
    }
}
