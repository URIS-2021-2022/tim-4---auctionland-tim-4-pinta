using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KatastarskaOpstinaAgregat.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KatastarskeOpstine",
                columns: table => new
                {
                    KatastarskaOpstinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivKatastarskeOpstine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatastarskeOpstine", x => x.KatastarskaOpstinaID);
                });

            migrationBuilder.InsertData(
                table: "KatastarskeOpstine",
                columns: new[] { "KatastarskaOpstinaID", "NazivKatastarskeOpstine" },
                values: new object[] { new Guid("3bd80c2a-c790-402f-b214-e3ebbc29d89f"), "Beocin" });

            migrationBuilder.InsertData(
                table: "KatastarskeOpstine",
                columns: new[] { "KatastarskaOpstinaID", "NazivKatastarskeOpstine" },
                values: new object[] { new Guid("177e64ad-2ff0-4a40-9c75-1f9b02ffe1e9"), "Beograd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KatastarskeOpstine");
        }
    }
}
