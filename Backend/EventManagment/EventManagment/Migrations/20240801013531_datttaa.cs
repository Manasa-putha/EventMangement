using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagment.Migrations
{
    public partial class datttaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventID1",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_EventID1",
                table: "Budgets",
                column: "EventID1",
                unique: true,
                filter: "[EventID1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventID1",
                table: "Budgets",
                column: "EventID1",
                principalTable: "Events",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventID1",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_EventID1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "EventID1",
                table: "Budgets");
        }
    }
}
