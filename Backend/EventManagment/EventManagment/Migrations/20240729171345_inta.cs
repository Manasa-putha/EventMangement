using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagment.Migrations
{
    public partial class inta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KW_Allowed = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    Expenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Budgets_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AlternativeNumber", "BasePrice", "City", "CreatedAt", "Email", "KW_Allowed", "MobileNumber", "Password", "PinCode", "RefreshToken", "RefreshTokenExpiryTime", "Token", "UpdatedAt", "UserType", "userName" },
                values: new object[,]
                {
                    { 1, "AP", "12345", 0, "Kadapa", new DateTime(2024, 6, 11, 13, 28, 12, 0, DateTimeKind.Unspecified), "admin@gmail.com", 0, "1234567890", "admin1234", "12345", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 7, 3, 9, 20, 12, 0, DateTimeKind.Unspecified), "Organizer", "Admin" },
                    { 2, "AP", "12345", 0, "Kurnool", new DateTime(2024, 3, 6, 13, 30, 12, 0, DateTimeKind.Unspecified), "sai@gmail.com", 0, "1234567890", "sai1234", "54321", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 6, 12, 9, 20, 12, 0, DateTimeKind.Unspecified), "Attendee", "sai" },
                    { 3, "AP", "12345", 0, "Guntur", new DateTime(2024, 1, 6, 14, 30, 12, 0, DateTimeKind.Unspecified), "manu@gmail.com", 0, "1234567890", "manu4321", "33333", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 9, 3, 9, 20, 12, 0, DateTimeKind.Unspecified), "Attendee", "manu" },
                    { 4, "AP", "12345", 0, "Anantpur", new DateTime(2024, 3, 6, 20, 40, 12, 0, DateTimeKind.Unspecified), "rani@gmail.com", 0, "1234567890", "rani43", "55555", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 6, 3, 9, 20, 12, 0, DateTimeKind.Unspecified), "Attendee", "Rani" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "Date", "Description", "EventName", "Location", "Time", "UserID" },
                values: new object[] { 1, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference about the latest in technology.", "Annual Tech Conference", "Convention Center", "10:00 AM", 1 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "Date", "Description", "EventName", "Location", "Time", "UserID" },
                values: new object[] { 2, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "A workshop focused on health and wellness.", "Health & Wellness Workshop", "Community Hall", "09:00 AM", 1 });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "ID", "EventID", "Expenses", "Revenue" },
                values: new object[] { 1, 1, 5000.00m, 10000.00m });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "ID", "EventID", "Expenses", "Revenue" },
                values: new object[] { 2, 2, 3000.00m, 7000.00m });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_EventID",
                table: "Budgets",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserID",
                table: "Events",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
