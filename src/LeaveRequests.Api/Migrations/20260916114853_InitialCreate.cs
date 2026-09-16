using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveRequests.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewerNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.CheckConstraint("CK_LeaveRequests_DateRange", "[EndDate] >= [StartDate]");
                    table.CheckConstraint("Ck_LeaveRequests_Status", "[Status] IN ('Pending', 'Approved', 'Rejected')");
                });

            migrationBuilder.InsertData(
                table: "LeaveRequests",
                columns: new[] { "Id", "CreatedAt", "EmployeeId", "EndDate", "ReviewerNote", "StartDate", "Status", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tasks Assigned to Employee E109 Zeyad Youssef", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", "Sick" },
                    { 2, new DateTime(2026, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", "Vacation" },
                    { 3, new DateTime(2026, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vacation max number of consecutive days is 7 days", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rejected", "Vacation" },
                    { 4, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Unpaid" },
                    { 5, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", "Vacation" },
                    { 6, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Unpaid" },
                    { 7, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2026, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Sick" },
                    { 8, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Vacation" },
                    { 9, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", "Unpaid" },
                    { 10, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", "Unpaid" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");
        }
    }
}
