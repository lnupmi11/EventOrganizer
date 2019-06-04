using Microsoft.EntityFrameworkCore.Migrations;

namespace EventOrganizer.Migrations
{
    public partial class CommentsAddContent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Categories",
                nullable: true);
        }
    }
}
