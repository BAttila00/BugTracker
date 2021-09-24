using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Dal.Migrations
{
    public partial class UpdatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedById",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedFinishDate",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProjectDescreption",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectStatus",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IssueId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descreption = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false),
                    IssueStatus = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AssignedToId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    IssuePriority = table.Column<int>(nullable: false),
                    IssueSeverity = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<int>(nullable: false),
                    SolvedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatorId",
                table: "Projects",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ModifiedById",
                table: "Projects",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IssueId",
                table: "Comments",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssignedToId",
                table: "Issue",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CreatorId",
                table: "Issue",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ModifiedById",
                table: "Issue",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProjectId",
                table: "Issue",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issue_IssueId",
                table: "Comments",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_CreatorId",
                table: "Projects",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_ModifiedById",
                table: "Projects",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issue_IssueId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_CreatorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_ModifiedById",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ModifiedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Comments_IssueId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PlannedFinishDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectDescreption",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectStatus",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Comments");
        }
    }
}
