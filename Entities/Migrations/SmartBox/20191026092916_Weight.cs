using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class Weight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "SmartBoxes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SmartBoxes");
        }
    }
}
