using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagment.Migrations
{
    public partial class datas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Guests_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "Date", "Description", "EventName", "Location", "Time", "UserID" },
                values: new object[] { 3, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Party to be fun", "Birthday Party", "Community Hall", "09:00 AM", 1 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "Date", "Description", "EventName", "Location", "Time", "UserID" },
                values: new object[] { 4, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fun filled Together night ", "Sangeeth Haldi functions", "Begmoeta Hall", "06:00 PM", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_EventID",
                table: "Guests",
                column: "EventID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
