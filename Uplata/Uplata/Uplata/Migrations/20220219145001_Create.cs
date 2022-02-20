using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplata.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                column: "Datum",
                value: new DateTime(2022, 2, 19, 15, 49, 59, 847, DateTimeKind.Local).AddTicks(7546));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("7a411c13-a195-48f7-8dbd-67596c3974c0"),
                column: "Datum",
                value: new DateTime(2022, 2, 19, 15, 49, 59, 855, DateTimeKind.Local).AddTicks(5588));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                column: "Datum",
                value: new DateTime(2022, 2, 7, 15, 36, 2, 89, DateTimeKind.Local).AddTicks(6851));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "UplataID",
                keyValue: new Guid("7a411c13-a195-48f7-8dbd-67596c3974c0"),
                column: "Datum",
                value: new DateTime(2022, 2, 7, 15, 36, 2, 91, DateTimeKind.Local).AddTicks(9895));
        }
    }
}
