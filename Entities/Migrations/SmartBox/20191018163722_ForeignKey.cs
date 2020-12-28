using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_SmartBoxes_SmartBoxId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_SmartBoxId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "SmartBoxId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Locations");

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "Locations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Locations_BoxId",
                table: "Locations",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_SmartBoxes_BoxId",
                table: "Locations",
                column: "BoxId",
                principalTable: "SmartBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_SmartBoxes_BoxId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_BoxId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "Locations");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Locations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "SmartBoxId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Locations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Locations_SmartBoxId",
                table: "Locations",
                column: "SmartBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_SmartBoxes_SmartBoxId",
                table: "Locations",
                column: "SmartBoxId",
                principalTable: "SmartBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
