using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Dal.Migrations
{
    public partial class SetNullUserOnComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
