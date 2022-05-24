using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class IncorrectColumnDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "SmartBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
