using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplaintAggregate.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Radnja_na_osnovu_zalbe_ID",
                keyValue: new Guid("07dea195-00ae-486a-8928-42921aec68bd"));

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Radnja_na_osnovu_zalbe_ID",
                keyValue: new Guid("820965da-e3fe-4ef2-bed4-7b131101f771"));

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("a9e48369-5661-41b7-bdf2-6059a0224554"));

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("eff33630-25b3-4e9e-a3ee-61e21226bb9c"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Status_zalbe",
                keyValue: new Guid("5cf8c011-349e-46dd-a668-cdc180a089a4"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Status_zalbe",
                keyValue: new Guid("698c3c91-33fc-4720-993f-f09bc1262814"));

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Tip_id",
                keyValue: new Guid("27b9e209-ca15-4f71-b815-a060a6c96c2b"));

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Tip_id",
                keyValue: new Guid("eebf3f9d-4457-41b4-a8aa-344f265f0109"));

            migrationBuilder.AddColumn<Guid>(
                name: "Radnja_na_osnovu_zalbe_ID",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Status_zalbe",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Tip_id",
                table: "Complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Radnja_na_osnovu_zalbe_ID", "JN_ide_u_krug_sa_novim_uslovima", "JN_ide_u_krug_sa_starim_uslovima", "JN_ne_ide_u_drugi_krug" },
                values: new object[,]
                {
                    { new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"), false, true, false },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), true, false, true }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Status_zalbe", "Odbijena", "Otvorena", "Usvojena" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), true, false, false },
                    { new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"), false, true, true }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Tip_id", "Zalba_na_odluku_o_davanju_na_koriscenje", "Zalba_na_odluku_o_davanju_na_zakup", "Zalba_na_tok_javnog_nadmetanja" },
                values: new object[,]
                {
                    { new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51"), "davanje na koriscenje", "davanje na zakup", "tok javnog nadmetanja" },
                    { new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"), "davanje na koriscenje1", "davanje na zakup1", "tok javnog nadmetanja1" }
                });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Radnja_na_osnovu_zalbe_ID", "Razlog_zalbe", "Status_zalbe", "Tip_id" },
                values: new object[] { new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51"), 32, 23, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2012, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje2", new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"), "razlog2", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51") });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ZalbaID", "Broj_nadmetanja", "Broj_rijesenja", "Datum_podnosenja_zalbe", "Datum_rijesenja", "Obrazlozenje", "Radnja_na_osnovu_zalbe_ID", "Razlog_zalbe", "Status_zalbe", "Tip_id" },
                values: new object[] { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), 32, 23, new DateTime(2011, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2011, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "obrazlozenje", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "razlog", new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"), new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd") });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_Radnja_na_osnovu_zalbe_ID",
                table: "Complaints",
                column: "Radnja_na_osnovu_zalbe_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_Status_zalbe",
                table: "Complaints",
                column: "Status_zalbe");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_Tip_id",
                table: "Complaints",
                column: "Tip_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Actions_Radnja_na_osnovu_zalbe_ID",
                table: "Complaints",
                column: "Radnja_na_osnovu_zalbe_ID",
                principalTable: "Actions",
                principalColumn: "Radnja_na_osnovu_zalbe_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Status_Status_zalbe",
                table: "Complaints",
                column: "Status_zalbe",
                principalTable: "Status",
                principalColumn: "Status_zalbe",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Types_Tip_id",
                table: "Complaints",
                column: "Tip_id",
                principalTable: "Types",
                principalColumn: "Tip_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Actions_Radnja_na_osnovu_zalbe_ID",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Status_Status_zalbe",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Types_Tip_id",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_Radnja_na_osnovu_zalbe_ID",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_Status_zalbe",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_Tip_id",
                table: "Complaints");

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "Complaints",
                keyColumn: "ZalbaID",
                keyValue: new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51"));

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Radnja_na_osnovu_zalbe_ID",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Radnja_na_osnovu_zalbe_ID",
                keyValue: new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Status_zalbe",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Status_zalbe",
                keyValue: new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"));

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Tip_id",
                keyValue: new Guid("c9e006af-bc13-49c7-ba4c-f2e2946301dd"));

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Tip_id",
                keyValue: new Guid("ec2e5d91-de9f-4af0-8fae-d8150e338c51"));

            migrationBuilder.DropColumn(
                name: "Radnja_na_osnovu_zalbe_ID",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Status_zalbe",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Tip_id",
                table: "Complaints");

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
    }
}
