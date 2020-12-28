using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class BoxState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoxState",
                table: "SmartBoxes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxState",
                table: "SmartBoxes");
        }
    }
}
