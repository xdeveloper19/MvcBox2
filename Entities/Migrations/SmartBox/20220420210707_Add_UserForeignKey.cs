using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class Add_UserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("FK_UserHasAccesses_User_UserId",
               "UserHasAccesses", "UserId",
               "AspNetUsers", principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_UserHasAccesses_User_UserId",
               "UserId", "UserHasAccesses");
        }
    }
}
