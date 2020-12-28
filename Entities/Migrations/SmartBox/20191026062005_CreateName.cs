using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class CreateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SmartBoxes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SmartBoxes");
        }
    }
}
