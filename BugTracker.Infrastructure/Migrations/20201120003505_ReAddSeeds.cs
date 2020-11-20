using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Infrastructure.Migrations
{
    public partial class ReAddSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "23f3e0c1-cf77-4efb-8a7a-331e045dd39f", "user@email.com", false, "John", "Smith", false, null, "USER@EMAIL.COM", "USER@EMAIL.COM", "AQAAAAEAACcQAAAAEKCVxLH1ituMVleFkEEfocyXYmiDZUX7yKZwg7egSk+BnmIDXjZRuXnmmO5wLz+UFA==", null, false, "76371424-8b9a-46bd-af5e-0de04d0475e3", false, "user@email.com" },
                    { 2, 0, "679bb4dd-0317-4acd-9623-e8626a7025e6", "user2@email.com", false, "Billy", "Bobson", false, null, "USER2@EMAIL.COM", "USER2@EMAIL.COM", "AQAAAAEAACcQAAAAEAYf/is3xqsSkGARt+2Y59jsGVI4JVR++WFG3jVEKl8G1x1XAClihZrbReZ+68O63w==", null, false, "ca90dd04-e770-431b-b4e0-d9394b481e89", false, "user2@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "OwnerId", "RepositoryUri", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(3051), "John Smith", "A web application for managing staffing", "Staffing App", 1, null, null, null },
                    { 2, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(4663), "John Smith", "A web application for managing quality control", "Qc App", 1, null, null, null },
                    { 3, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(4695), "John Smith", "An internal web app for measuring agent performance", "Scorecard App", 1, null, null, null },
                    { 4, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(4699), "John Smith", "A web application for agents to monitor performance", "Scoreboard App", 1, null, null, null },
                    { 5, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(4703), "John Smith", "A web app for error tracking", "Error Tracker", 1, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToId", "CreatedAt", "CreatedBy", "Description", "Priority", "ProjectId", "RequestorId", "Status", "Title", "Type" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(6559), "John Smith", "I've attempted to update his team numerous times but to no avail. Can you take a look?", 2, 1, 1, 0, "Joe Borski's team will not update", 2 },
                    { 2, 1, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(8462), "John Smith", "Can you check why she isn't loading for this month?", 2, 1, 1, 0, "Susan Jones isn't showing up for this month", 2 },
                    { 3, 1, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(8504), "John Smith", "We'd like to filter by employee last name. Is this something you guys can add to the app?", 0, 1, 1, 2, "Filter by employee last name", 1 },
                    { 4, 2, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(8508), "John Smith", "Staffing is due by close of business and noone can access the app.", 3, 1, 1, 1, "The app is down!", 2 },
                    { 5, 0, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(8512), "John Smith", "I can't load the staffing app.", 3, 1, 1, 0, "Is this app working?", 2 },
                    { 6, 2, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(8517), "John Smith", "There are only 5 records to qc for today. Is this correct?", 2, 2, 2, 1, "Only 5 records for today", 2 },
                    { 7, 0, new DateTime(2020, 11, 19, 19, 35, 4, 944, DateTimeKind.Local).AddTicks(8521), "John Smith", "We'll need to add a check to see if all the records are being pulled in from the call center.", 1, 2, 2, 0, "Add a check for total records", 0 }
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
                values: new object[] { 1, new DateTime(2020, 11, 19, 19, 35, 4, 945, DateTimeKind.Local).AddTicks(9), "John Smith", 1, "I'm troubleshooting this now", 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatorId", "Description", "TicketId" },
                values: new object[] { 2, new DateTime(2020, 11, 19, 19, 35, 4, 945, DateTimeKind.Local).AddTicks(986), "Billy Bobson", 2, "Ok let me know what you find!", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
