using Microsoft.EntityFrameworkCore.Migrations;

namespace KatastarskaOpstinaAgregat.Migrations
{
    public partial class InitalCreateOpstn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NazivKatastarskeOpstine",
                table: "KatastarskeOpstine",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NazivKatastarskeOpstine",
                table: "KatastarskeOpstine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
