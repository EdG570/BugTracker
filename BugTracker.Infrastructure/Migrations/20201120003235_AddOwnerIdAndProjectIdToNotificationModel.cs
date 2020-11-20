using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Infrastructure.Migrations
{
    public partial class AddOwnerIdAndProjectIdToNotificationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserProjects",
                keyColumns: new[] { "AppUserId", "ProjectId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserProjects",
                keyColumns: new[] { "AppUserId", "ProjectId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "UserProjects",
                keyColumns: new[] { "AppUserId", "ProjectId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "UserProjects",
                keyColumns: new[] { "AppUserId", "ProjectId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "UserProjects",
                keyColumns: new[] { "AppUserId", "ProjectId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
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

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectOwnerId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ProjectOwnerId",
                table: "Notifications");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "b9cf14db-5dcb-471b-8554-b17764ad9dd7", "user@email.com", false, "John", "Smith", false, null, "USER@EMAIL.COM", "USER@EMAIL.COM", "AQAAAAEAACcQAAAAEK1U6hZfE0/yJG/0dTqFCuNpxWaDqWCCCJP8bP1SbRUWw1Y1cCRn2t1WVn8SaXfrxw==", null, false, "414b4496-809a-425b-85ce-ac33405fd4a1", false, "user@email.com" },
                    { 2, 0, "b6128f32-088a-4c1c-9304-b7d6a9db1011", "user2@email.com", false, "Billy", "Bobson", false, null, "USER2@EMAIL.COM", "USER2@EMAIL.COM", "AQAAAAEAACcQAAAAEKgl1VGGZZQDVVEWFqfVYNc9T2172S1XFWgeNm7+D0LsbdlvMFP3nz2gfOuQEMySDQ==", null, false, "10f723ea-ec54-4ed9-84af-7ee42fcce7aa", false, "user2@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "OwnerId", "RepositoryUri", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 11, 15, 11, 14, 46, 568, DateTimeKind.Local).AddTicks(9233), "John Smith", "A web application for managing staffing", "Staffing App", 1, null, null, null },
                    { 2, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(935), "John Smith", "A web application for managing quality control", "Qc App", 1, null, null, null },
                    { 3, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(971), "John Smith", "An internal web app for measuring agent performance", "Scorecard App", 1, null, null, null },
                    { 4, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(976), "John Smith", "A web application for agents to monitor performance", "Scoreboard App", 1, null, null, null },
                    { 5, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(979), "John Smith", "A web app for error tracking", "Error Tracker", 1, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToId", "CreatedAt", "CreatedBy", "Description", "Priority", "ProjectId", "RequestorId", "Status", "Title", "Type" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(2883), "John Smith", "I've attempted to update his team numerous times but to no avail. Can you take a look?", 2, 1, 1, 0, "Joe Borski's team will not update", 2 },
                    { 2, 1, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(4803), "John Smith", "Can you check why she isn't loading for this month?", 2, 1, 1, 0, "Susan Jones isn't showing up for this month", 2 },
                    { 3, 1, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(4858), "John Smith", "We'd like to filter by employee last name. Is this something you guys can add to the app?", 0, 1, 1, 2, "Filter by employee last name", 1 },
                    { 4, 2, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(4863), "John Smith", "Staffing is due by close of business and noone can access the app.", 3, 1, 1, 1, "The app is down!", 2 },
                    { 5, 0, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(4867), "John Smith", "I can't load the staffing app.", 3, 1, 1, 0, "Is this app working?", 2 },
                    { 6, 2, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(4871), "John Smith", "There are only 5 records to qc for today. Is this correct?", 2, 2, 2, 1, "Only 5 records for today", 2 },
                    { 7, 0, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(4875), "John Smith", "We'll need to add a check to see if all the records are being pulled in from the call center.", 1, 2, 2, 0, "Add a check for total records", 0 }
                });

            migrationBuilder.InsertData(
                table: "UserProjects",
                columns: new[] { "AppUserId", "ProjectId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatorId", "Description", "TicketId" },
                values: new object[] { 1, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(6336), "John Smith", 1, "I'm troubleshooting this now", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatorId", "Description", "TicketId" },
                values: new object[] { 2, new DateTime(2020, 11, 15, 11, 14, 46, 569, DateTimeKind.Local).AddTicks(7201), "Billy Bobson", 2, "Ok let me know what you find!", 1 });
        }
    }
}
