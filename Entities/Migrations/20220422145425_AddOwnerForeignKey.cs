using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddOwnerForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("FK_UserHasAccesses_Owner_OwnerId",
               "UserHasAccesses", "OwnerId",
               "Owners", principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_UserHasAccesses_Owner_OwnerId",
               "OwnerId", "UserHasAccesses");
        }
    }
}
