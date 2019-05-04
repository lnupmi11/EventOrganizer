using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOrganizer.Migrations
{
    public partial class RenameEventsColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDateTime",
                table: "Events",
                newName: "ScheduledAt");

            migrationBuilder.RenameColumn(
                name: "CreationalDateTime",
                table: "Events",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Events",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScheduledAt",
                table: "Events",
                newName: "EventDateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Events",
                newName: "CreationalDateTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "EventId");
        }
    }
}
