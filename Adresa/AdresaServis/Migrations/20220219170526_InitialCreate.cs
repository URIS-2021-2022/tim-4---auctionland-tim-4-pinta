﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdresaServis.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    DrzavaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NazivDrzave = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.DrzavaID);
                });

            migrationBuilder.CreateTable(
                name: "Adrese",
                columns: table => new
                {
                    AdresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Broj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrzavaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adrese", x => x.AdresaID);
                    table.ForeignKey(
                        name: "FK_Adrese_Drzave_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Drzave",
                columns: new[] { "DrzavaID", "NazivDrzave" },
                values: new object[] { new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"), "Srbija" });

            migrationBuilder.InsertData(
                table: "Drzave",
                columns: new[] { "DrzavaID", "NazivDrzave" },
                values: new object[] { new Guid("2b7558a6-f3f4-460d-80e0-26e1c037f455"), "Crna Gora" });

            migrationBuilder.InsertData(
                table: "Adrese",
                columns: new[] { "AdresaID", "Broj", "DrzavaID", "Mesto", "PostanskiBroj", "Ulica" },
                values: new object[] { new Guid("9a8e31d5-5e7b-46e7-80c6-f22e607ee907"), "50", new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"), "Beograd", "11000", "Karadjordjeva" });

            migrationBuilder.InsertData(
                table: "Adrese",
                columns: new[] { "AdresaID", "Broj", "DrzavaID", "Mesto", "PostanskiBroj", "Ulica" },
                values: new object[] { new Guid("723123b1-3ab1-4741-9437-c8a1d6ad20da"), "4", new Guid("fd5e46de-290f-4844-a004-4a94ae24f654"), "Novi Sad", "21000", "Strazilovska" });

            migrationBuilder.CreateIndex(
                name: "IX_Adrese_DrzavaID",
                table: "Adrese",
                column: "DrzavaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adrese");

            migrationBuilder.DropTable(
                name: "Drzave");
        }
    }
}
