using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagment.Migrations
{
    public partial class dataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventID",
                table: "Budgets",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventID",
                table: "Budgets",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserID",
                table: "Events",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
