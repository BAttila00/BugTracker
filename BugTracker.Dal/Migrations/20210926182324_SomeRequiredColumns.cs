using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Dal.Migrations
{
    public partial class SomeRequiredColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issue_IssueId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Users_AssignedToId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Users_CreatorId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Users_ModifiedById",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Projects_ProjectId",
                table: "Issue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issue",
                table: "Issue");

            migrationBuilder.RenameTable(
                name: "Issue",
                newName: "Issues");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_ProjectId",
                table: "Issues",
                newName: "IX_Issues_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_ModifiedById",
                table: "Issues",
                newName: "IX_Issues_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_CreatorId",
                table: "Issues",
                newName: "IX_Issues_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_AssignedToId",
                table: "Issues",
                newName: "IX_Issues_AssignedToId");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descreption",
                table: "Issues",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                table: "Issues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issues_IssueId",
                table: "Comments",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssignedToId",
                table: "Issues",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_CreatorId",
                table: "Issues",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_ModifiedById",
                table: "Issues",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issues_IssueId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssignedToId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_CreatorId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_ModifiedById",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_ProjectId",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "Issues",
                newName: "Issue");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_ProjectId",
                table: "Issue",
                newName: "IX_Issue_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_ModifiedById",
                table: "Issue",
                newName: "IX_Issue_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_CreatorId",
                table: "Issue",
                newName: "IX_Issue_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issue",
                newName: "IX_Issue_AssignedToId");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descreption",
                table: "Issue",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issue",
                table: "Issue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issue_IssueId",
                table: "Comments",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Users_AssignedToId",
                table: "Issue",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Users_CreatorId",
                table: "Issue",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Users_ModifiedById",
                table: "Issue",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Projects_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
