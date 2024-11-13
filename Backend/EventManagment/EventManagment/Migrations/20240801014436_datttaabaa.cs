using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagment.Migrations
{
    public partial class datttaabaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventID1",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_EventID",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_EventID1",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Guests",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Guests",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_EventID",
                table: "Guests",
                newName: "IX_Guests_EventId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Events",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EventID1",
                table: "Budgets",
                newName: "EventId1");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Budgets",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Budgets",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_EventID",
                table: "Budgets",
                newName: "IX_Budgets_EventId");

            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_EventId1",
                table: "Guests",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_EventId1",
                table: "Budgets",
                column: "EventId1",
                unique: true,
                filter: "[EventId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventId",
                table: "Budgets",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventId1",
                table: "Budgets",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_EventId",
                table: "Guests",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_EventId1",
                table: "Guests",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventId1",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_EventId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_EventId1",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_EventId1",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_EventId1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Guests",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Guests",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_EventId",
                table: "Guests",
                newName: "IX_Guests_EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "EventId1",
                table: "Budgets",
                newName: "EventID1");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Budgets",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Budgets",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_EventId",
                table: "Budgets",
                newName: "IX_Budgets_EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_EventID1",
                table: "Budgets",
                column: "EventID1",
                unique: true,
                filter: "[EventID1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventID",
                table: "Budgets",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventID1",
                table: "Budgets",
                column: "EventID1",
                principalTable: "Events",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_EventID",
                table: "Guests",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
