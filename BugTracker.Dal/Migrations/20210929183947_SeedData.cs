using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Dal.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreationDate", "CreatorId", "ModifiedById", "ModifiedOn", "PlannedFinishDate", "ProjectDescreption", "ProjectName", "ProjectStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Just a project", "Proj01", 0 },
                    { 2, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "asp .net core project", "Proj02", 0 },
                    { 3, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "java project", "Proj03", 0 },
                    { 4, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "android project", "Proj04", 0 }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "AssignedToId", "CreationDate", "CreatorId", "Descreption", "IssuePriority", "IssueSeverity", "IssueStatus", "ModifiedById", "ModifiedOn", "ProjectId", "SolvedOn" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Issue01", 0, 0, 1, 1, new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Issue02", 0, 0, 0, 1, new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Issue03", 0, 0, 0, 1, new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2021, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Issue03", 0, 0, 1, 1, new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Issues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
