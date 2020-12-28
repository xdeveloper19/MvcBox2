using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class InitialBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsOpenedBox = table.Column<bool>(nullable: false),
                    IsOpenedDoor = table.Column<bool>(nullable: false),
                    Light = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Temperature = table.Column<double>(nullable: false),
                    Wetness = table.Column<double>(nullable: false),
                    BatteryPower = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Altitude = table.Column<double>(nullable: true),
                    SignalLevel = table.Column<double>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false),
                    SmartBoxId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_SmartBoxes_SmartBoxId",
                        column: x => x.SmartBoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_SmartBoxId",
                table: "Locations",
                column: "SmartBoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "SmartBoxes");
        }
    }
}
